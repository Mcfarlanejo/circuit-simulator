using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltMeter : MonoBehaviour
{
    public GameObject settingSwitch; // the object to be rotated


    private void OnMouseDown()
    {

        settingSwitch.transform.Rotate(Vector3.up, 20f); // rotate 1 degree around the y-axis
    }

    
}
