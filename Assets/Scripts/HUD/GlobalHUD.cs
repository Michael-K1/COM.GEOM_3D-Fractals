
using UnityEngine;
using UnityEngine.UI;

public class GlobalHUD : MonoBehaviour{
    public FractalVisualizer fv;
    // public Vector3 defaultPos, defaultEulerAngle, defaultScale;
    public Toggle showPlane;
    private void Awake(){
        var c = Camera.main;
        if (c == null) return;
        fv = c.GetComponent<FractalVisualizer>();
        // defaultPos = fv.transform.localPosition;
        // defaultEulerAngle = fv.transform.localEulerAngles;
        // defaultScale = fv.transform.localScale;
    }
    
    private void Start(){
        showPlane.isOn = fv.showPlane;
    }

    private void LateUpdate(){
        fv.showPlane = showPlane.isOn;
    }

    // public void ResetCamera(){
    //     fv.transform.position = defaultPos;
    //     fv.transform.eulerAngles = defaultEulerAngle;
    //     fv.transform.localScale = defaultScale;
    //     
    //     print("aaaa");
    // }
}
