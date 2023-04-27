using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random; // Add this line to import the Random class

public class Cable : MonoBehaviour
{
    public float volts;
    public float amps;

    public AnchorPoint[] anchorPoints;

    private LineRenderer lineRenderer;

    public int pointCount = 10;
    public float ropeLength = 1f;
    public float curveAmount = 1f;
    public float variance = 0.1f;

    private void Awake()
    {
        anchorPoints = new AnchorPoint[2];
    }

    private void Start()
{
    lineRenderer = GetComponent<LineRenderer>();

    // Set the width of the line renderer
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 0.1f;

    // Set the position count of the line renderer
    lineRenderer.positionCount = pointCount;

    // Generate a rope-like curve for the line renderer
    float distanceBetweenPoints = ropeLength / (pointCount - 1);

    for (int i = 0; i < pointCount; i++)
    {
        float x = i * distanceBetweenPoints;
        float y = Mathf.Sin(Mathf.PI * i / (pointCount - 1)) * curveAmount;

        x += Range(-variance, variance); // Use Range() instead of Random.Range()
        y -= Range(-variance, variance); // Reverse the direction of the curve

        lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
    }

    // Set the first and last positions of the line renderer to the start and end positions of the anchor points
    lineRenderer.SetPosition(0, anchorPoints[0].transform.position);
    lineRenderer.SetPosition(pointCount - 1, anchorPoints[1].transform.position);

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
    }
}
