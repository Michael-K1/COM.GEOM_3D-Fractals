using UnityEngine;

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
    
    [Header("Tetrahedron")]
    public Vector3 tetrahedronPos;
    [Range(1, 50)]
    public int tetraIterations;
    [Range(1.00001f, 2.50000f)]
    public float tetraScale;
    public Color tetraColor;

    [Header("Paxis")] 
    public Vector3 paxisPos;
    public Color paxisColor;
    public  bool paxisSwapA, paxisSwapB;
    [Range(1, 3)]
    public int paxisIter1;
    [Range(1, 20)]
    public int paxisIter2;
    [Range(.1f, .29f)]
    public float paxisMult;
    
    #endregion

    private void OnRenderImage(RenderTexture src, RenderTexture dest){
        if (!RayMarchMaterial){
            Graphics.Blit(src,dest);
            return;
            
        }
        //view setup
        RayMarchMaterial.SetMatrix("CamFrustum",CamFrustum(ThisCamera));
        RayMarchMaterial.SetMatrix("CamToWorld", ThisCamera.cameraToWorldMatrix);
        
        //raymarch setup
        RayMarchMaterial.SetInt("maxRaySteps", MAX_STEPS);
        RayMarchMaterial.SetFloat("maxRayDistance", MAX_DISTANCE);
        RayMarchMaterial.SetFloat("ACCURACY", ACCURACY);
        
        
        //LIGHTING
        RayMarchMaterial.SetVector("_LightDir", directionalLight?directionalLight.forward: Vector3.down);
        RayMarchMaterial.SetColor("_LightColor",lightColor);
        RayMarchMaterial.SetFloat("_LightIntensity",lightIntensity);
        
        //shadows
        RayMarchMaterial.SetFloat("_ShadowIntensity", shadowIntensity);
        RayMarchMaterial.SetFloat("_ShadowPenumbra", shadowPenumbra);
        RayMarchMaterial.SetVector("_ShadowDistance", shadowDistance);
        
        //Ambient Occlusion
        RayMarchMaterial.SetFloat("_AOStepSize", aoStepSize);
        RayMarchMaterial.SetFloat("_AOIntensity", aoIntensity);
        RayMarchMaterial.SetInt("_AOIteration", aoIteration);
        
        //Environment
        RayMarchMaterial.SetColor("groundColor", groundColor);
        RayMarchMaterial.SetFloat("_ColorIntensity", colorIntensity );
        RayMarchMaterial.SetFloat("shapeBlending", shapeBlending );
        
        //TETRAHEDRON
        RayMarchMaterial.SetVector("tetraPos",tetrahedronPos);
        RayMarchMaterial.SetFloat("tetraScale", tetraScale);
        RayMarchMaterial.SetInt("tetraIterations", tetraIterations);
        RayMarchMaterial.SetColor("tetraColor",tetraColor);
        
        //PAXIS
        RayMarchMaterial.SetVector("paxisPos", paxisPos);
        RayMarchMaterial.SetColor("paxisColor", paxisColor);
        RayMarchMaterial.SetFloat("paxisMult", paxisMult);
        RayMarchMaterial.SetInt("paxisIter1", paxisSwapB ? Mathf.Clamp(paxisIter1,1,2): paxisIter1);
        RayMarchMaterial.SetInt("paxisIter2", paxisIter2);
        RayMarchMaterial.SetInt("paxisIter1Swap", paxisSwapA ? 1 : 0);
        RayMarchMaterial.SetInt("paxisIter2Swap", paxisSwapB? 1 : 0);

        RenderTexture.active = dest;
        RayMarchMaterial.SetTexture("_MainTex", src);
        
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
