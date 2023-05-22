using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoltMeter : MonoBehaviour
{
    public GameObject settingSwitch; // the object to be rotated

    private bool isMouseOver = false;

    private void Update()
    {
        if (isMouseOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                settingSwitch.transform.Rotate(Vector3.up, 15f); // rotate 15 degrees around the y-axis
            }
            else if (Input.GetMouseButtonDown(1))
            {
                settingSwitch.transform.Rotate(Vector3.up, -15f); // rotate -15 degrees around the y-axis (opposite direction)
            }
        }
    }

    private void OnMouseOver()
    {
        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        isMouseOver = false;
    }
}
