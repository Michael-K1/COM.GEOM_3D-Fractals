
using UnityEngine;

public class GlobalHUD : MonoBehaviour{
    public FractalVisualizer fv;
    private void Awake(){
        if (Camera.main != null) 
            fv = Camera.main.GetComponent<FractalVisualizer>();
    }
}
