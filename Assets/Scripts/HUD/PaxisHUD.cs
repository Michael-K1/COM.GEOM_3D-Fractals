using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaxisHUD : FractalHUD{
    [Header("Paxis")]
    public Slider scale;
    public Toggle swapA, swapB,animate;
    public Slider iter1, iter2, mult;
    public TMP_Text scaleLabel,iter1Label, iter2Label, multLabel;

    void Start(){
        BaseSetup(Fv.showPaxis, Fv.paxisColor,Fv.paxisPos);
        swapA.isOn = Fv.paxisSwapA;
        swapB.isOn = Fv.paxisSwapB;
        animate.isOn = Fv.animatePaxis;
        
        scale.value = Fv.paxisScale;
        mult.value = Fv.paxisMult;
        iter1.value = Fv.paxisIter1;
        iter2.value = Fv.paxisIter2;
        
    }

    
    void LateUpdate(){
        BaseUpdate(ref Fv.showPaxis, ref Fv.paxisColor,ref Fv.paxisPos);

        Fv.paxisSwapA = swapA.isOn;
        Fv.paxisSwapB = swapB.isOn;
        Fv.animatePaxis = animate.isOn;
        
        Fv.paxisScale = scale.value;
        Fv.paxisMult = mult.value;
        Fv.paxisIter1 = (int)iter1.value;
        Fv.paxisIter2 = (int)iter2.value;

        iter1Label.text = $"Iterations [1]\t\t{Fv.paxisIter1}";
        iter2Label.text = $"Iterations [2]\t\t{Fv.paxisIter2}";
        multLabel.text = $"Multiplier\t\t{Fv.paxisMult}";
        scaleLabel.text = $"Scale\t\t\t{Fv.paxisScale}";
        
    }
}
