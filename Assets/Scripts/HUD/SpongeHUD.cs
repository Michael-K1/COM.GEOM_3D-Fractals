using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpongeHUD : FractalHUD{
    [Header("Menger Sponge")] public Toggle animate;
    public Slider iter;
    public TMP_Text iterLabel;
    
    void Start(){
        BaseSetup(Fv.showMengerSponge, Fv.spongeColor, Fv.spongePos); 
        
        iter.value = Fv.spongeIter;
        animate.isOn = Fv.animateMengerSponge;
    }

    
    void LateUpdate(){
        BaseUpdate(ref Fv.showMengerSponge,ref Fv.spongeColor, ref Fv.spongePos); 

        Fv.animateMengerSponge = animate.isOn;
        Fv.spongeIter = (int)iter.value;
        iterLabel.text = $"Iterations\t{Fv.spongeIter}";
        

    }
}
