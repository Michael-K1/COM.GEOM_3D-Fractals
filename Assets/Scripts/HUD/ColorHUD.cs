using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHUD : MonoBehaviour{
   

    public Slider red, green, blue;
    

    public void SetUp(Color c){
        red.value = c.r;
        green.value = c.g;
        blue.value = c.b;
    
    }
    public void UpdateColor(ref Color c){
        
        c.r = red.value;
        c.g = green.value;
        c.b = blue.value;
    }
}
