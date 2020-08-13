using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionHUD : MonoBehaviour
{
    public Slider posX, posY, posZ;

    public void SetUpPosition(Vector3 v){
        posX.value = v.x;
        posY.value = v.y;
        posZ.value = v.z;
    }

    // Update is called once per frame
    public void UpdatePosition(ref Vector3 v){
        v.x = posX.value;
        v.y = posY.value;
        v.z = posZ.value;
    }
}
