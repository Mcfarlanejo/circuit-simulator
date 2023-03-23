using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;

public class CircuitBreaker : Component
{
    public Vector3 onPosition;
    public Vector3 offPosition;
    public GameObject switchObject;
    public bool switchOn = false;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        AssignValues();
        CheckStartPosition();
        switchObject.transform.localPosition = target;
    }

    public override void Interact()
    {
        if (!switchOn)
        {
            target = onPosition;
            volts = STANDARDVOLTAGE;
            switchOn = true;
        }
        else
        {
            target = offPosition;
            volts = 0;
            switchOn = false;
        }
        switchObject.transform.localPosition = target;
    }

    void CheckStartPosition()
    {
        switchOn = switchObject.transform.localPosition == onPosition ? true : false;
        target = switchOn ? onPosition : offPosition;
    }
}
