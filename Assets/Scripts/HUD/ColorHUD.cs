using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ColorHUD : MonoBehaviour{
   

    public Slider red, green, blue;
    public TMP_Text rVal, gVal, bVal;

    public void SetUp(Color c){
        red.value = c.r;
        green.value = c.g;
        blue.value = c.b;
    
    }
    public void UpdateColor(ref Color c){
        
        c.r = red.value;
        
        c.g = green.value;
        gVal.text = $"G:{c.g}";
        c.b = blue.value;

        Color32 p = c;
        rVal.text = $"R: {p.r}";
        gVal.text = $"G: {p.g}";
        bVal.text = $"B: {p.b}";
    }
}
