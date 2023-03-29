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

    private void Awake()
    {
        powerSource = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        AssignValues();
        CheckStartPosition();
        switchObject.transform.localPosition = target;
    }
    private void FixedUpdate()
    {
        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            anchorPoint.volts = volts;
            anchorPoint.amps = amps;
        }
    }

    public override void Interact()
    {
        if (!switchOn)
        {
            target = onPosition;
            volts = STANDARDVOLTAGE;
            amps = STANDARDAMPS;
            switchOn = true;
        }
        else
        {
            target = offPosition;
            volts = 0;
            amps = 0;
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
