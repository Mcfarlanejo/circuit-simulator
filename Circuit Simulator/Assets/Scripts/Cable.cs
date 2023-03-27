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
    }

    private void Update()
    {
        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            if (anchorPoint.volts != 0)
            {
                volts = Mathf.Max(anchorPoints[0].volts, anchorPoints[1].volts);
                amps = Mathf.Max(anchorPoints[0].amps, anchorPoints[1].amps);
            }
            else
            {
                anchorPoint.volts = volts;
                anchorPoint.amps = amps;
            }
        }
       
    }
}
