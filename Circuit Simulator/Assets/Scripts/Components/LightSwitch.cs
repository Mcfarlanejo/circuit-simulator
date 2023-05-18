using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Component
{
    public float onAngle;
    public float offAngle;
    public GameObject switchObject;
    public bool switchOn = false;
    private Quaternion target;
    public GameObject connectedLight;

    new private void Start()
    {
        base.Start();
        
        CheckStartPosition();
        
        switchObject.transform.rotation = target;

        componentName = "Light Switch";
    }

    private void FixedUpdate()
    {
        if (switchOn && !fault)
        {
            connectedLight.SetActive(true);
        }
        else
        {
            connectedLight.SetActive(false);
        }
    }
    public override void Interact()
    {
        if (!switchOn)
        {
            target = Quaternion.Euler(onAngle, 0, 0);
            SwitchTriggered();
        }
        else
        {
            target = Quaternion.Euler(-offAngle, 0, 0);
            SwitchTriggered();
        }
        switchObject.transform.rotation = target;
    }

    void CheckStartPosition()
    {
        switchOn = volts == expectedVoltage ? true : false;
        target = connectedLight.activeInHierarchy ? Quaternion.Euler(onAngle, 0, 0) : Quaternion.Euler(-offAngle, 0, 0);
        connectedLight.SetActive(switchOn);
    }

    void SwitchTriggered()
    {   
        switchOn = !switchOn;
    }
}
