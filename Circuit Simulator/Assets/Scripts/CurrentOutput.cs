using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentOutput : MonoBehaviour
{
    public AnchorPoint positive;
    public AnchorPoint negative;     

    public Text text;

    public GameObject settings;

    private const float SWITCH_ANGLE_THRESHOLD = 90f;

    // Show voltage or current output of connected object
    void Update()
    {
        if (negative.attachedCables != null && settings != null)
        {
            float switchAngle = settings.transform.rotation.eulerAngles.y;
            if (switchAngle > 0f && switchAngle <= SWITCH_ANGLE_THRESHOLD)
            {
                ShowVolts();
            }
            else
            {
                ShowAmps();
            }
        }
    }

    public void ShowAmps()
    {
        if (positive.attachedCables != null)
        {
            foreach (AnchorPoint anchorPoint in positive.attachedCables[0].anchorPoints)
            {
                if (anchorPoint.gameObject.name != "+AnchorPoint")
                {
                    text.text = anchorPoint.amps.ToString();
                }
            }
        }
    }

    public void ShowVolts()
    {
        if (positive != null)
        {
            foreach (AnchorPoint anchorPoint in positive.attachedCables[0].anchorPoints)
            {
                if (anchorPoint.gameObject.name != "+AnchorPoint")
                {
                    text.text = anchorPoint.volts.ToString();
                }
            }
        }
    }
}

