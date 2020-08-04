using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class FractalVisualizer : MonoBehaviour{
    [SerializeField] 
    private Shader rayMarchingShader;
    private Material rayMarchMat;
    public Material RayMarchMaterial{
        get{
            if (!rayMarchMat && rayMarchingShader)
                rayMarchMat = new Material(rayMarchingShader){hideFlags = HideFlags.HideAndDontSave};//doesn't get dispose by garbage collector
            return rayMarchMat;
        }
    }

    private Camera _cam;
    public Camera thisCamera{
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
    public int MAX_STEPS;
    [Range(.1f, .001f)] 
    public float ACCURACY;

    #endregion

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

    [Header("Signed Distance Field")] //here are the parameters for the fractals
    public Color groundColor;
    //tetrahedron
    public Vector4 tetrahedronPosAndScale;
    [Range(1, 100)]
    public int tetraIterations;
    #endregion

    private void OnRenderImage(RenderTexture src, RenderTexture dest){
        
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
