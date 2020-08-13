using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnvironmentHUD : MonoBehaviour{
    public FractalVisualizer fv;
    [Header("RayMarching")]
    public Slider maxDistanceSlider;
    public TMP_Text maxDistanceLabel;
    public Slider maxStepsSlider;     
    public TMP_Text maxStepsLabel;    
  
    public Slider AccuracySlider; 
    public TMP_Text AccuracyLabel;
    
    public ColorHUD GroundColor;
    public Slider ColorIntensitySlider, ShapeBlendingSlider;

    void Start(){
        fv = GetComponentInParent<GlobalHUD>().fv;
        //raymarch
        maxDistanceSlider.value = fv.MAX_DISTANCE;
        maxDistanceLabel.text = fv.MAX_DISTANCE.ToString();

        maxStepsSlider.value=fv.MAX_STEPS;
        maxStepsLabel.text = fv.MAX_STEPS.ToString();

        AccuracySlider.value = fv.ACCURACY;
        AccuracyLabel.text = fv.ACCURACY.ToString();

        //environment
        GroundColor.SetUp(fv.groundColor);
        ColorIntensitySlider.value = fv.colorIntensity;
        ShapeBlendingSlider.value = fv.shapeBlending;
        
        //lighting

    }
    
    private void LateUpdate(){
        UpdateRaymarch();

        UpdateEnvironment();

        UpdateLighting();
    }


    private void UpdateRaymarch(){
        fv.MAX_DISTANCE = maxDistanceSlider.value;
        maxDistanceLabel.text = $"Render Distance\t{maxDistanceSlider.value}";

        fv.MAX_STEPS = (int) maxStepsSlider.value;
        maxStepsLabel.text = $"Render Steps\t{maxStepsSlider.value}";

        fv.ACCURACY = AccuracySlider.value;
        AccuracyLabel.text = $"Accuracy\t\t{AccuracySlider.value}";
    }

    private void UpdateEnvironment(){
        GroundColor.SetColor(ref fv.groundColor);
        fv.colorIntensity = ColorIntensitySlider.value;
        fv.shapeBlending = ShapeBlendingSlider.value;
    }

    private void UpdateLighting(){
        
        
    }
}
