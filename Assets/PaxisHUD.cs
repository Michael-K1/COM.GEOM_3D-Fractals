using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaxisHUD : FractalHUD{
    [Header("Paxis")]
    public Slider scale;

    public Slider iter1, iter2, mult;
    public Toggle swapA, SwapB,animate;
    
    void Start(){
        showFractal.isOn = Fv.showPaxis;

        swapA.isOn = Fv.paxisSwapA;
        SwapB.isOn = Fv.paxisSwapB;
        animate.isOn = Fv.animatePaxis;
        
        scale.value = Fv.paxisScale;
        mult.value = Fv.paxisMult;
        iter1.value = Fv.paxisIter1;
        iter2.value = Fv.paxisIter2;
        
        FractalColor.SetUp(Fv.paxisColor);
        FractalPos.SetUpPosition(Fv.paxisPos);
    }

    
    void LateUpdate(){
        Fv.showPaxis = showFractal.isOn;

        Fv.paxisSwapA = swapA.isOn;
        Fv.paxisSwapB = SwapB.isOn;
        Fv.animatePaxis = animate.isOn;
        
        Fv.paxisScale = scale.value;
        Fv.paxisMult = mult.value;
        Fv.paxisIter1 = (int)iter1.value;
        Fv.paxisIter2 = (int)iter2.value;
        
        FractalColor.UpdateColor(ref Fv.paxisColor);
        FractalPos.UpdatePosition(ref Fv.paxisPos);
    }
}
