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

    private ColorHUD fractalColor;

    public ColorHUD FractalColor{
        get{
            if (!fractalColor)
                fractalColor = GetComponentInChildren<ColorHUD>();
            return fractalColor;
        } 
    }

    private PositionHUD fractalPos;

    public PositionHUD FractalPos{
        get{
            if (!fractalPos)
                fractalPos = GetComponentInChildren<PositionHUD>();
            return fractalPos;
        }
    }

}
