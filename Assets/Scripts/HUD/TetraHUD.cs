
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TetraHUD : FractalHUD{
    [Header("Tetrahedron")]
    public Slider tetraIterations;
    public Slider tetraScale;
    public TMP_Text iter, scale;
    private void Start(){
        BaseSetup(Fv.showTetra,Fv.tetraColor,Fv.tetrahedronPos);
        
        
        tetraIterations.value = Fv.tetraIterations;
        tetraScale.value = Fv.tetraScale;
        
    }

    private void LateUpdate(){
        BaseUpdate(ref Fv.showTetra,ref Fv.tetraColor,ref Fv.tetrahedronPos);

        Fv.tetraIterations =(int) tetraIterations.value;
        Fv.tetraScale = tetraScale.value;
        iter.text = $"Iterations\t\t{Fv.tetraIterations}";
        scale.text = $"Scale\t\t\t{Fv.tetraScale}";
        
        
    }
}
