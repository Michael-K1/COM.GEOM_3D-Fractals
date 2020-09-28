
using UnityEngine;
public static class ShaderLookup{
    public static readonly int Frustum = Shader.PropertyToID("CamFrustum");
    public static readonly int CamToWorld = Shader.PropertyToID("CamToWorld");
    public static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    //raymarch
    public static readonly int MaxRaySteps = Shader.PropertyToID("maxRaySteps");
    public static readonly int MaxRayDistance = Shader.PropertyToID("maxRayDistance");
    public static readonly int Accuracy = Shader.PropertyToID("ACCURACY");
    //LIGHTING
    public static readonly int LightDir = Shader.PropertyToID("_LightDir");
    public static readonly int LightColor = Shader.PropertyToID("_LightColor");
    public static readonly int LightIntensity = Shader.PropertyToID("_LightIntensity");
    //SHADOWS
    public static readonly int ShadowIntensity = Shader.PropertyToID("_ShadowIntensity");
    public static readonly int ShadowPenumbra = Shader.PropertyToID("_ShadowPenumbra");
    public static readonly int ShadowDistance = Shader.PropertyToID("_ShadowDistance");
    //AMBIENT OCCLUSION
    public static readonly int AoStepSize = Shader.PropertyToID("_AOStepSize");
    public static readonly int AoIntensity = Shader.PropertyToID("_AOIntensity");
    public static readonly int AoIteration = Shader.PropertyToID("_AOIteration");
    //ENVIRONMENT
    public static readonly int GroundColor = Shader.PropertyToID("groundColor");
    public static readonly int ColorIntensity = Shader.PropertyToID("_ColorIntensity");
    public static readonly int ShapeBlending = Shader.PropertyToID("shapeBlending");
    public static readonly int ShowPlane = Shader.PropertyToID("showPlane");
    
    //TETRAHEDRON
    public static readonly int ShowTetra = Shader.PropertyToID("showTetra");
    public static readonly int TetraPos = Shader.PropertyToID("tetraPos");
    public static readonly int TetraScale = Shader.PropertyToID("tetraScale");
    public static readonly int TetraIterations = Shader.PropertyToID("tetraIterations");
    public static readonly int TetraColor = Shader.PropertyToID("tetraColor");
    
    //PAXIS
    public static readonly int ShowPaxis = Shader.PropertyToID("showPaxis");
    public static readonly int PaxisPos = Shader.PropertyToID("paxisPos");
    public static readonly int PaxisColor = Shader.PropertyToID("paxisColor");
    public static readonly int PaxisMult = Shader.PropertyToID("paxisMult");
    public static readonly int PaxisScale = Shader.PropertyToID("paxisScale");
    public static readonly int PaxisIter1 = Shader.PropertyToID("paxisIter1");
    public static readonly int PaxisIter2 = Shader.PropertyToID("paxisIter2");
    public static readonly int PaxisIter1Swap = Shader.PropertyToID("paxisIter1Swap");
    public static readonly int PaxisIter2Swap = Shader.PropertyToID("paxisIter2Swap");
    public static readonly int AnimatePaxis = Shader.PropertyToID("animatePaxis");
    
    //MANDELBULB
    public static readonly int MandelPos = Shader.PropertyToID("mandelPos");
    public static readonly int ShowMandelBulb = Shader.PropertyToID("showMandelBulb");
    public static readonly int AnimateMandel = Shader.PropertyToID("animateMandel");
    public static readonly int MandelDynamicColor = Shader.PropertyToID("mandelDynamicColor");
    public static readonly int MandelStaticColor = Shader.PropertyToID("mandelStaticColor");
    public static readonly int MandelIter = Shader.PropertyToID("mandelIter");
    public static readonly int RotateMandelBulb = Shader.PropertyToID("rotateMandelBulb");
    public static readonly int AlternativeMandelBulb = Shader.PropertyToID("alternativeMandelBulb");
    
    //MENGER SPONGE
    public static readonly int ShowSponge = Shader.PropertyToID("showSponge");
    public static readonly int SpongePos = Shader.PropertyToID("spongePos");
    public static readonly int AnimateSponge = Shader.PropertyToID("animateSponge");
    public static readonly int SpongeColor = Shader.PropertyToID("spongeColor");
    public static readonly int SpongeIterations = Shader.PropertyToID("spongeIterations");
    
    //MANDELBOX
    public static readonly int ShowMandelBox = Shader.PropertyToID("showMandelBox");
    public static readonly int AnimateMandelBox = Shader.PropertyToID("animateMandelBox");
    public static readonly int MandelBoxColor = Shader.PropertyToID("mandelBoxColor");
    public static readonly int MandelBoxPos = Shader.PropertyToID("mandelBoxPos");
    public static readonly int MandelBoxIter = Shader.PropertyToID("mandelBoxIter");
    public static readonly int MandelBoxScale = Shader.PropertyToID("mandelBoxScale");
    public static readonly int MandelBoxSize = Shader.PropertyToID("mandelBoxSize");
    public static readonly int MandelBoxMinRadius = Shader.PropertyToID("mandelBoxMinRadius");
    public static readonly int MandelBoxFixedRadius = Shader.PropertyToID("mandelBoxFixedRadius");

    
}
