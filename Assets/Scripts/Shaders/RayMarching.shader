Shader "Fractals/RayMarching"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 5.0

            #include "UnityCG.cginc"
            #include "FractalsDistanceEstimation.cginc"
            #include "Operations.cginc"
            
            
            //from c# script
            sampler2D _MainTex;
            uniform sampler2D _CameraDepthTexture;
            uniform float4x4 CamFrustum;    //ray direction based on the field of view and aspect ratio of the camera
            uniform float4x4 CamToWorld;    //matrix to convert to worldspace
            
            //RayMarching parameters
            uniform int maxRaySteps;       //max iteration in the RayMarching function
            uniform float maxRayDistance;   //max distance the ray can travel
            uniform float ACCURACY;         //accuracy of the ray near an object
            
            
            //lighting
            uniform float3 _LightDir, _LightColor;       //forward direction of the light and color
            uniform float _LightIntensity;
            uniform float _ShadowIntensity, _ShadowPenumbra;
            uniform float2 _ShadowDistance;
            uniform float _AOStepSize, _AOIntensity;
            uniform int _AOIteration;
            uniform float _ColorIntensity;
            
            //ground
            uniform fixed4 groundColor;
            uniform float shapeBlending;
            
            //Sierpinski tetrahedron
            uniform int showTetra;
            uniform fixed4 tetraColor;
            uniform float3 tetraPos;
            uniform float tetraScale;
            uniform int tetraIterations;
            
            //paxis
            uniform int showPaxis;
            uniform fixed4 paxisColor;
            uniform int animatePaxis;
            uniform float3 paxisPos;
            uniform int paxisIter1, paxisIter2, paxisIter1Swap, paxisIter2Swap;
            uniform float paxisMult, paxisScale;
            
            //mandelbulb
            uniform int showMandel, animateMandel, mandelDynamicColor;
            uniform fixed4 mandelStaticColor;
            uniform float3 mandelPos;
            uniform int mandelIter;
            
            //Menger sponge
            uniform int showSponge;
            uniform fixed4 spongeColor; 
            uniform float3 spongePos;
            uniform int spongeIterations;
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 ray :TEXCOORD1;
                
            };

            v2f vert (appdata v)
            {
                v2f o;
                half index =v.vertex.z;
                v.vertex.z=0;
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                o.ray=CamFrustum[(int)index].xyz;
                o.ray/=abs(o.ray.z);     //normalized in z direction
                o.ray=mul(CamToWorld, o.ray);   //convert the ray to world space
                
                return o;
            }
            
            float4 distanceField(float3 pos){
                float4 combine=float4(groundColor.rgb, sdPlane(pos, float4(0, 1, 0, 0)));;
                if(showMandel==1){
                    float4 mandel=float4(mandelStaticColor.rgb, sdMandelbulb3D(pos-mandelPos.xyz, mandelIter, animateMandel));
                    //combine=float4(mandelStaticColor.rgb, sdMandelbulb3D(pos-mandelPos.xyz, mandelIter, animateMandel));
                    //return mandel;
                    combine=opUS(combine, mandel, shapeBlending);
                }
                
                if(showSponge==1){
                    float4 sponge=float4(spongeColor.rgb, sdSponge(pos-spongePos.xyz, spongeIterations));
                    combine=opUS(combine, sponge, shapeBlending);
                }
                
                if(showTetra==1){
                    float4 tetra=float4(tetraColor.rgb, sdTetrahedron(pos-tetraPos.xyz, tetraIterations,tetraScale));
                    combine=opUS(combine, tetra, shapeBlending);
                }
                if(showPaxis==1){
                    float4 paxis=float4(paxisColor.rgb, sdPaxis(pos-paxisPos.xyz, paxisIter1, paxisIter2, paxisMult, paxisIter1Swap==0, paxisIter2Swap==0, paxisScale, animatePaxis==1));
                    combine= opUS(combine, paxis, shapeBlending);
                }
             
                return combine;
                
            }
            
            float3 getNormal(float3 p){     //get the normal by offsetting the pos 
                //const float2 offset=float2(0.001, 0.0);
                const float2 offset=float2(0.001, -0.001);
                float3 n=float3(
                            distanceField(p+offset.xyy).w - distanceField(p-offset.xyy).w,
                            distanceField(p+offset.yxy).w - distanceField(p-offset.yxy).w,
                            distanceField(p+offset.yyx).w - distanceField(p-offset.yyx).w
                        );
                        
                return normalize(n);
            }
            
            float SoftShadow(float3 ro, float3 rd, float minT, float maxT, float k){ //k= penumbra parameter
    
                float result=1.0;
                
                for(float t=minT; t < maxT;){
                    float h=distanceField(ro+rd*t).w;  //if h<0 we are inside a distance field object
                    if(h<0.001)
                        return 0.0;
                    
                    result=min(result, k*h/t); //always between 0 and 1
                    t+=h;
                }
                return result;
        
            }
        
            float AmbientOcclusion(float3 pos, float3 n){
                float step=_AOStepSize;
                float ao=0.0;
                float dist;
                
                for(int i=1;i <=_AOIteration; i++){
                    dist=step*i;
                    ao +=max(0.0, (dist - distanceField(pos+n*dist).w)/dist);
                }
                return (1.0- ao*_AOIntensity);
            }
        
            float3 Shading(float3 pos, float3 n, fixed3 c){
                float3 result;
                //Diffuse Color
                float3 color=c.rgb *_ColorIntensity;
                
                //Directional Light
                float3 light=(_LightColor * dot(-_LightDir, n) *0.5 +0.5) * _LightIntensity;
                
                //Shadows    
                float shadow=SoftShadow(pos, -_LightDir, _ShadowDistance.x, _ShadowDistance.y, _ShadowPenumbra) * 0.5+0.5;
                shadow=max(0.0, pow(shadow, _ShadowIntensity));
                
                //Ambient Occlusion
                float ao=AmbientOcclusion(pos, n);
                
                result=color * light * shadow * ao;
                
                return result;
            }
            float g=0;
            float3 mandelColor(float3 ro, float3 rd, float r){
                float3 c = float3(0,0,0);
                float3 cs = 0.5 + 0.5*cos(_Time.y+rd.xyx+float3(0,2,4));
                
                if(r>0.)
                {
                    float3 p = ro+rd*r;
                    float3 n = getNormal(p);
                    float3 sun = normalize(float3(0.2,0.5,0.3));
                    float dif = clamp(dot(sun,n),0.0,1.0);
                    float sky = clamp(0.5+0.5*dot(n,float3(0,1,0)),0.,1.);
                    
                    c=float3(r,r,r)*-.1;
                    c+=cs*r*dif;
                    c+=r*sky*float3(0.9,0.5,0.5);
                    
                }
                
                
                return c+(g/75.*cs);
            }
            
            bool RayMarching(float3 rayOrigin, float3 rayDirection, float depth, float maxRayDist, int maxIteration, inout float3 pos, inout fixed3 dColor){
                bool hit;
                float t=0; //distance traveled along the ray direction
                
                for(int i=0;i<maxIteration;i++ ){
                    if(t>maxRayDist || t>= depth){//create the environment where the ray hits no target
                        hit=false;
                        break;
                    }
                    
                    pos= rayOrigin + rayDirection * t;//current position of the ray
                    
                      //check for hit in distance field
                    float4 d=distanceField(pos);
                    
                    if(mandelDynamicColor==1 && showMandel==1)
                        g+=0.1/(0.1+d.w*d.w);
                    
                    if(d.w<ACCURACY){//the ray has hit something
                        if(mandelDynamicColor==1 && showMandel==1){
                            float3 col=mandelColor(rayOrigin, rayDirection, t);
                            dColor=fixed3(col.rgb);                  
                        }else
                            dColor=d.rgb;
                        hit= true;
                        break;
                    }
                    t+=d.w;
                }
                return hit;
            }
            
            
            
            fixed4 frag (v2f i) : SV_Target{
                float depth=LinearEyeDepth(tex2D(_CameraDepthTexture, i.uv).r);
                depth*=length(i.ray);
                
                fixed4 col=tex2D(_MainTex, i.uv);
                
                float3 rayDirection=normalize(i.ray.xyz);
                float3 rayOrigin=_WorldSpaceCameraPos;
                
                fixed4 result;
                float3 hitPosition;
                float3 dColor;
                
                bool hit=RayMarching(rayOrigin, rayDirection, depth, maxRayDistance, maxRaySteps, hitPosition, dColor);
                if(hit){ 
                    //shading!
                    float3 n=getNormal(hitPosition);
                    float3 s=Shading(hitPosition, n, dColor);
                    result=fixed4(s, 1);  
                }else
                    result=fixed4(0,0,0,0);
                

                
                return fixed4(col * (1.0- result.w) + result.xyz * result.w, 1.0 );             
            }
            ENDCG
        }
    }
}
