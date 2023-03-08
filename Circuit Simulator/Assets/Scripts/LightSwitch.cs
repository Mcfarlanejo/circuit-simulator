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

    private void Start()
    {
        AssignValues();
        target = Quaternion.Euler(-offAngle, 0, 0);
        switchObject.transform.rotation = target;
    }
    public override void Interact()
    {
        if (!switchOn)
        {
            target = Quaternion.Euler(onAngle, 0, 0);
            switchOn = !switchOn;
        }
        else
        {
            target = Quaternion.Euler(-offAngle, 0, 0);
            switchOn = !switchOn;
        }
        switchObject.transform.rotation = target;
    }
}
