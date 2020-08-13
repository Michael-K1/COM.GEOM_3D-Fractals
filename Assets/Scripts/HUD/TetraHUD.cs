
using UnityEngine;
using UnityEngine.UI;


public class TetraHUD : FractalHUD{
    [Header("Tetrahedron")]
    public Slider tetraIterations;
    public Slider tetraScale;
    private void Start(){
        
        showFractal.isOn = Fv.showTetra;
        
        tetraIterations.value = Fv.tetraIterations;
        tetraScale.value = Fv.tetraScale;
        FractalColor.SetUp(Fv.tetraColor);
        FractalPos.SetUpPosition(Fv.tetrahedronPos);
    }

    private void LateUpdate(){
        Fv.showTetra = showFractal.isOn;

        Fv.tetraIterations =(int) tetraIterations.value;
        Fv.tetraScale = tetraScale.value;
        
        FractalColor.UpdateColor(ref Fv.tetraColor);
        FractalPos.UpdatePosition(ref Fv.tetrahedronPos);
    }
}
