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

    private void Start()
    {
        AssignValues();
        CheckStartPosition();
        
        switchObject.transform.rotation = target;
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
        switchOn = connectedLight.activeInHierarchy ? true : false;
        target = connectedLight.activeInHierarchy ? Quaternion.Euler(onAngle, 0, 0) : Quaternion.Euler(-offAngle, 0, 0);
    }

    void SwitchTriggered()
    {
        if (!fault)
        {
            connectedLight.SetActive(!connectedLight.activeInHierarchy);
        }
        
        switchOn = !switchOn;
    }
}
