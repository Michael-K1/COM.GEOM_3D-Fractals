
using UnityEngine;
using UnityEngine.UI;


public class TetraHUD : FractalHUD{
    [Header("Tetrahedron")]
    public Slider tetraIterations;
    public Slider tetraScale;
    private void Start(){
        
        showFractal.isOn = Fv.showTetra;
        posX.value = Fv.tetrahedronPos.x;
        posY.value = Fv.tetrahedronPos.y;
        posZ.value = Fv.tetrahedronPos.z;
        tetraIterations.value = Fv.tetraIterations;
        tetraScale.value = Fv.tetraScale;
        fractalColor.SetUp(Fv.tetraColor);
    }

    private void LateUpdate(){
        Fv.showTetra = showFractal.isOn;
        Fv.tetrahedronPos.x = posX.value;
        Fv.tetrahedronPos.y = posY.value;
        Fv.tetrahedronPos.z = posZ.value;
        Fv.tetraIterations =(int) tetraIterations.value;
        Fv.tetraScale = tetraScale.value;

    }
}
