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
            anchorPoint.attachedCables.Add(gameObject.GetComponent<Cable>());
        }
    }

    private void Update()
    {
        

        if ((anchorPoints[0].gameObject.name != "-AnchorPoint") && (anchorPoints[1].gameObject.name != "-AnchorPoint") &&
            (anchorPoints[0].gameObject.name != "+AnchorPoint") && (anchorPoints[1].gameObject.name != "+AnchorPoint"))
        {
            if (((anchorPoints[0].powerSource == true) && (anchorPoints[0].volts != 0)) ||
                ((anchorPoints[1].powerSource == true) && (anchorPoints[1].volts != 0)))
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
            else
            {
                volts = 0;
                amps = 0;
                foreach (AnchorPoint anchorPoint in anchorPoints)
                {
                    anchorPoint.volts = volts;
                    anchorPoint.amps = amps;
                }
            }
        }
        else
        {
            foreach (AnchorPoint anchorPoint in anchorPoints)
            {
                if ((anchorPoint.gameObject.name == "+AnchorPoint") || (anchorPoint.gameObject.name == "-AnchorPoint"))
                {
                    anchorPoint.volts = volts;
                    anchorPoint.amps = amps;
                }
            }
        }
    }
}