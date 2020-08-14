using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoxHUD : FractalHUD{
    [Header("Mandel Box")]
    public Toggle animate;

    public Slider iter, scale, size;
    public TMP_Text iterL, scaleL, sizeL;
    void Start(){
        BaseSetup(Fv.showMandelBox, Fv.mandelBoxColor, Fv.mandelBoxPos);

        animate.isOn = Fv.animateMandelBox;
        iter.value = Fv.mandelBoxIter;
        scale.value = Fv.mandelBoxScale;
        size.value = Fv.mandelBoxSize;
    }

    // Update is called once per frame
    void LateUpdate(){
        BaseUpdate(ref Fv.showMandelBox, ref Fv.mandelBoxColor, ref Fv.mandelBoxPos);
        
        Fv.animateMandelBox=animate.isOn;
        Fv.mandelBoxIter=(int)iter.value;
        Fv.mandelBoxScale=scale.value;
        Fv.mandelBoxSize=size.value;
    }
}
