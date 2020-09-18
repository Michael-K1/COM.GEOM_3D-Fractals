using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsHUD : MonoBehaviour{
    private void Start(){
        gameObject.SetActive(false);
    }

    public void ShowCommands(){
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
