using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public float volts;
    public float amps;

    public AnchorPoint[] anchorPoints;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        anchorPoints = new AnchorPoint[2];
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            anchorPoint.attachedCable = gameObject.GetComponent<Cable>();
        }
    }

    private void Update()
    {
        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            if (anchorPoint.powerSource || anchorPoint.transferPower)
            {
                volts = anchorPoint.volts;
                amps = anchorPoint.amps;
            }
            else
            {
                anchorPoint.volts = volts;
                anchorPoint.amps = amps;
            }
        }
    }
}
