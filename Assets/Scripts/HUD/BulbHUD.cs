using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BulbHUD : FractalHUD{

    [Header("MandelBulb")] 
    public Toggle animate;
    public Toggle rotate, alter;
    public Slider iter;
    public TMP_Text iterLabel;
    void Start(){
        BaseSetup(Fv.showMandelBulb, Fv.mandelColor, Fv.mandelPos);
        
        animate.isOn = Fv.animateMandel;
        rotate.isOn = Fv.rotateMandelBulb;
        alter.isOn = Fv.alternativeMandelBulb;
        iter.value = Fv.mandelIter;
    }

    
    void LateUpdate(){
        BaseUpdate(ref Fv.showMandelBulb, ref Fv.mandelColor, ref Fv.mandelPos);
        
        Fv.animateMandel = animate.isOn;
        Fv.rotateMandelBulb = rotate.isOn;
        Fv.alternativeMandelBulb = alter.isOn;
        Fv.mandelIter =(int) iter.value;
        iterLabel.text = $"Iterations\t\t{Fv.mandelIter}";
    }
}
