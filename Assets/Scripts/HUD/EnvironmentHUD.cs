using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnvironmentHUD : MonoBehaviour{
    public FractalVisualizer fv;
    [Header("RayMarching")]
    public Slider accuracySlider; 
    public TMP_Text accuracyLabel;
    
    public ColorHUD groundColor;
    public Slider colorIntensitySlider, shapeBlendingSlider;
    
    [Header("Lighting")]
    public ColorHUD lightColor;
    public Slider lightIntensity;
    
    [Header("Shadow")]
    public Slider shadowIntensity;
    public Slider shadowPenumbra;

    [Header("Ambient Occlusion")] 
    public Slider AOIntensity;
    public Slider AOSteps, AOIterations;
    void Start(){
        fv = GetComponentInParent<GlobalHUD>().fv;
        //raymarch
        accuracySlider.value = fv.ACCURACY;
        accuracyLabel.text = $"Accuracy\t\t{accuracySlider.value}";

        //environment
        groundColor.SetUp(fv.groundColor);
        colorIntensitySlider.value = fv.colorIntensity;
        shapeBlendingSlider.value = fv.shapeBlending;
        
        //lighting
        lightColor.SetUp(fv.lightColor);
        lightIntensity.value = fv.lightIntensity;
        
        //shadow
        shadowIntensity.value = fv.shadowIntensity;
        shadowPenumbra.value = fv.shadowPenumbra;
        
        //AO
        AOSteps.value = fv.aoStepSize;
        AOIterations.value = fv.aoIteration;
        AOIntensity.value = fv.aoIntensity;
    }
    
    private void LateUpdate(){
        UpdateRaymarch();

        UpdateEnvironment();

        UpdateLighting();
    }


    private void UpdateRaymarch(){
        fv.ACCURACY = accuracySlider.value;
        accuracyLabel.text = $"Accuracy\t\t{accuracySlider.value}";
    }

    private void UpdateEnvironment(){
        groundColor.UpdateColor(ref fv.groundColor);
        fv.colorIntensity = colorIntensitySlider.value;
        fv.shapeBlending = shapeBlendingSlider.value;
    }

    private void UpdateLighting(){
        lightColor.UpdateColor(ref fv.lightColor);
        fv.lightIntensity = lightIntensity.value;

        fv.shadowIntensity = shadowIntensity.value;
        fv.shadowPenumbra = shadowPenumbra.value;

        fv.aoStepSize = AOSteps.value;
        fv.aoIntensity = AOIntensity.value;
        fv.aoIteration =(int) AOIterations.value;
        
    }
}
