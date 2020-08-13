using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FractalHUD : MonoBehaviour{
    [SerializeField]
    private FractalVisualizer fv;

    protected FractalVisualizer Fv{
        get{
            if (!fv)
                fv=GetComponentInParent<GlobalHUD>().fv;
            return fv;
        }
    }

    public Toggle showFractal;
    public Slider posX, posY, posZ;
    public ColorHUD fractalColor;
    
}
