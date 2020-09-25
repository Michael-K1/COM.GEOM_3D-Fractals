using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageHUD : MonoBehaviour{
    public bool isEng;
    private TMP_Text buttonText;
    private List<GameObject> engPanels, itaPanels;

    private void Start(){
        engPanels = new List<GameObject>(GameObject.FindGameObjectsWithTag("Lang/Eng"));
        itaPanels = new List<GameObject>(GameObject.FindGameObjectsWithTag("Lang/Ita"));

        buttonText = GetComponentInChildren<TMP_Text>();

        isEng = !isEng;
        ActivateCorrectLanguage();
    }

    public void ActivateCorrectLanguage(){
        isEng = !isEng;
        buttonText.text = isEng ? "ITA" : "ENG";
        
      
        SetActivation(isEng, engPanels);
        SetActivation(!isEng, itaPanels);
   
    }

    public void SetActivation(bool v, List<GameObject> l){
        
        l.ForEach(g=>g.SetActive(v));
    }
}
