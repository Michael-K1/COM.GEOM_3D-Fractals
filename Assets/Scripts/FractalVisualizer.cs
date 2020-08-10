using UnityEngine;
using static  ShaderLookup;
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class FractalVisualizer : MonoBehaviour{
    [SerializeField] 
    private Shader rayMarchingShader;
    private Material rayMarchMat;

    private Material RayMarchMaterial{
        get{
            if (!rayMarchMat && rayMarchingShader)
                rayMarchMat = new Material(rayMarchingShader){hideFlags = HideFlags.HideAndDontSave};//doesn't get dispose by garbage collector
            return rayMarchMat;
        }
    }

    private Camera _cam;

    private Camera ThisCamera{
        get{
            if (!_cam)
                _cam = GetComponent<Camera>();
            return _cam;
        }
    }

    #region RayMarch

    [Header("RayMarching Setup")]
    [Range(1, 1000)]
    public float MAX_DISTANCE;
    [Range(1, 400)] 
    public int MAX_STEPS=164;
    [Range(.1f, .001f)] 
    public float ACCURACY;

    #endregion
    
    [Header("Environment")]
    public Color groundColor;
    [Range(0,4)]
    public float colorIntensity;
    [Range(0,4)]
    public float shapeBlending;
    #region Lighting

    [Header("Directional Light")] 
    public Transform directionalLight;
    public Color lightColor;
    [Range(0, 4)] 
    public float lightIntensity;

    [Header("Shadows")] 
    [Range(0, 4)] 
    public float shadowIntensity;
    [Range(1, 128)]
    public float shadowPenumbra;
    public Vector2 shadowDistance;
        
    [Header("Ambient Occlusion")]
    [Range(.01f, 10f)]
    public float aoStepSize;
    [Range(1, 5)]
    public int aoIteration;
    [Range(0, 1)]
    public float aoIntensity;

    #endregion

    #region Fractals Parameter

    //[Header("Signed Distance Field"),Space] //here are the parameters for the fractals
    
    [Header("Sierpinski Tetrahedron")] 
    public bool showTetra;
    public Vector3 tetrahedronPos;
    [Range(1, 25)]
    public int tetraIterations;
    [Range(1.00001f, 2.50000f)]
    public float tetraScale;
    public Color tetraColor;

    [Header("Paxis")] 
    public bool showPaxis;
    public Vector3 paxisPos;
    public Color paxisColor;
    public  bool paxisSwapA, paxisSwapB,animatePaxis;
    [Range(1, 3)]
    public int paxisIter1;
    [Range(1, 20)]
    public int paxisIter2;
    [Range(.1f, 1f)]
    public float paxisMult;
    [Range(1f, 5f)]
    public float paxisScale;

    [Header("MandelBulb3D")] 
    public bool showMandel;
    public Color mandelColor;
    public bool  animateMandel, mandelDynamicColor;
    public Vector3 mandelPos;
    [Range(1, 10)] 
    public int mandelIter;

    [Header("Menger Sponge")] 
    public bool showMengerSponge;
    public Color spongeColor;
    public Vector3 spongePos;
    [Range(0,6)]
    public int spongeIter;



    #endregion
    
    private void OnRenderImage(RenderTexture src, RenderTexture dest){
        if (!RayMarchMaterial){
            Graphics.Blit(src,dest);
            return;
        }
/*
        if (showMandel)
            showTetra = showPaxis = !showMandel;
        */
        //view setup
        RayMarchMaterial.SetMatrix(Frustum,CamFrustum(ThisCamera));
        RayMarchMaterial.SetMatrix(CamToWorld, ThisCamera.cameraToWorldMatrix);
        
        //raymarch setup
        RayMarchMaterial.SetInt(MaxRaySteps, MAX_STEPS);
        RayMarchMaterial.SetFloat(MaxRayDistance, MAX_DISTANCE);
        RayMarchMaterial.SetFloat(Accuracy, ACCURACY);
        
        
        //LIGHTING
        RayMarchMaterial.SetVector(LightDir, directionalLight?directionalLight.forward: Vector3.down);
        RayMarchMaterial.SetColor(LightColor,lightColor);
        RayMarchMaterial.SetFloat(LightIntensity,lightIntensity);
        
        //shadows
        RayMarchMaterial.SetFloat(ShadowIntensity, shadowIntensity);
        RayMarchMaterial.SetFloat(ShadowPenumbra, shadowPenumbra);
        RayMarchMaterial.SetVector(ShadowDistance, shadowDistance);
        
        //Ambient Occlusion
        RayMarchMaterial.SetFloat(AoStepSize, aoStepSize);
        RayMarchMaterial.SetFloat(AoIntensity, aoIntensity);
        RayMarchMaterial.SetInt(AoIteration, aoIteration);
        
        //Environment
        RayMarchMaterial.SetColor(GroundColor, groundColor);
        RayMarchMaterial.SetFloat(ColorIntensity, colorIntensity );
        RayMarchMaterial.SetFloat(ShapeBlending, shapeBlending );
        
        //Sierpinski TETRAHEDRON
        RayMarchMaterial.SetInt(ShowTetra, showTetra?1:0);
        RayMarchMaterial.SetVector(TetraPos,tetrahedronPos);
        RayMarchMaterial.SetFloat(TetraScale, tetraScale);
        RayMarchMaterial.SetInt(TetraIterations, tetraIterations);
        RayMarchMaterial.SetColor(TetraColor,tetraColor);
        
        //PAXIS
        RayMarchMaterial.SetInt(ShowPaxis, showPaxis?1:0);
        RayMarchMaterial.SetVector(PaxisPos, paxisPos);
        RayMarchMaterial.SetColor(PaxisColor, paxisColor);
        RayMarchMaterial.SetFloat(PaxisMult, paxisMult);
        RayMarchMaterial.SetFloat(PaxisScale, paxisScale);
        RayMarchMaterial.SetInt(PaxisIter1, paxisSwapB ? Mathf.Clamp(paxisIter1,1,2): paxisIter1);
        RayMarchMaterial.SetInt(PaxisIter2, paxisIter2);
        RayMarchMaterial.SetInt(PaxisIter1Swap, paxisSwapA ? 1 : 0);
        RayMarchMaterial.SetInt(PaxisIter2Swap, paxisSwapB? 1 : 0);
        RayMarchMaterial.SetInt(AnimatePaxis, animatePaxis? 1 : 0);

        //MANDELBULB
        RayMarchMaterial.SetVector(MandelPos, mandelPos);
        RayMarchMaterial.SetInt(ShowMandel, showMandel?1:0);
        RayMarchMaterial.SetInt(AnimateMandel, animateMandel?1:0);
        RayMarchMaterial.SetInt(MandelDynamicColor, mandelDynamicColor?1:0);
        RayMarchMaterial.SetColor(MandelStaticColor, mandelColor);
        RayMarchMaterial.SetInt(MandelIter, mandelIter);
        
        //MENGER SPONGE
        RayMarchMaterial.SetInt(ShowSponge,showMengerSponge?1:0);
        RayMarchMaterial.SetVector(SpongePos,spongePos);
        RayMarchMaterial.SetColor(SpongeColor, spongeColor);
        RayMarchMaterial.SetInt(SpongeIterations,spongeIter);
        
        RenderTexture.active = dest;
        RayMarchMaterial.SetTexture(MainTex, src);
        
        GL.PushMatrix();
        GL.LoadOrtho();
        RayMarchMaterial.SetPass(0);
        GL.Begin(GL.QUADS);
        
        //BottomLeft of the quad
        GL.MultiTexCoord2(0,.0f,.0f);
        GL.Vertex3(.0f,.0f,3.0f);
        //BR of the quad
        GL.MultiTexCoord2(0,1.0f,.0f);
        GL.Vertex3(1.0f,.0f,2.0f);
        //TR of the quad
        GL.MultiTexCoord2(0,1.0f,1.0f);
        GL.Vertex3(1.0f,1.0f,1.0f);
        //TL of the quad
        GL.MultiTexCoord2(0,.0f,1.0f);
        GL.Vertex3(.0f,1.0f,0.0f);
        
        GL.End();
        GL.PopMatrix();

    }

    private Matrix4x4 CamFrustum(Camera c){
        Matrix4x4 frustum = Matrix4x4.identity;

        float fov = Mathf.Tan(c.fieldOfView * .5f * Mathf.Deg2Rad);
        
        Vector3 goUp = Vector3.up * fov;
        Vector3 goRight = Vector3.right * fov * c.aspect;

        Vector3 TL = -Vector3.forward - goRight + goUp;    //topLeft
        Vector3 TR = -Vector3.forward + goRight + goUp;
        Vector3 BL = -Vector3.forward - goRight - goUp;    //bottomLeft
        Vector3 BR = -Vector3.forward + goRight - goUp;

        frustum.SetRow(0,TL);
        frustum.SetRow(1,TR);
        frustum.SetRow(2,BR);
        frustum.SetRow(3,BL);
        
        return frustum;
    }
}
