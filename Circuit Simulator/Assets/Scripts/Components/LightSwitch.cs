using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        
        CheckStartPosition();
        
        switchObject.transform.rotation = target;
    }

    private void FixedUpdate()
    {
        CheckForPower();
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

    public void CheckForPower()
    {
        if (switchOn && !fault && connectedLight.GetComponentInParent<LightBlub>().screwedIn)
        {
            connectedLight.SetActive(true);
            connectedLight.GetComponentInParent<LightBlub>().UpdateMaterial(true);
        }
        else
        {
            connectedLight.SetActive(false);
            connectedLight.GetComponentInParent<LightBlub>().UpdateMaterial(false);
        }
    }

}
