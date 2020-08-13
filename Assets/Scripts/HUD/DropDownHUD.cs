using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropDownHUD : MonoBehaviour{
    public List<GameObject> elements=new List<GameObject>();

    private void Start(){
        elements[0].SetActive(true);
        for (int i = 1; i < elements.Count; i++)
            elements[i].SetActive(false);
    }

    public void SwitchOnPanel(int v){
        for (int i = 0; i < elements.Count; i++)
            elements[i].SetActive(i==v);
        
    }
}
