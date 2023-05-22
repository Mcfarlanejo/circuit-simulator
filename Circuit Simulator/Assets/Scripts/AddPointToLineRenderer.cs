using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class AddPointToLineRenderer : MonoBehaviour
{
    public int numPointsToAdd = 4; // The number of points to add between the start and end points
    public float droopAmount = 0.5f; // The amount of droop to apply to the line renderer

    private Vector3 endPoint;

    private LineRenderer lineRenderer;

    private void Start()
    {
        // Get a reference to the LineRenderer component on this object
        lineRenderer = GetComponent<LineRenderer>();
        Spawned();
    }

    private void Spawned()
    {
        endPoint = lineRenderer.GetPosition(1);

        // Calculate the spacing between the points to add
        float segmentLength = Vector3.Distance(lineRenderer.GetPosition(0), lineRenderer.GetPosition(1));
        float segmentSpacing = segmentLength / (numPointsToAdd + 1);

        // Add the new points evenly spaced between the start and end points
        for (int i = 1; i <= numPointsToAdd; i++)
        {
            float t = i / (float)(numPointsToAdd + 1);
            Vector3 newPoint = Vector3.Lerp(lineRenderer.GetPosition(0), endPoint, t);

            // Gradually move the y-axis down towards the middle
            float offsetY = Mathf.Sin(t * Mathf.PI) * droopAmount;
            newPoint.y -= offsetY;

            lineRenderer.positionCount = lineRenderer.positionCount + 1;
            lineRenderer.SetPosition(lineRenderer.positionCount - 2, newPoint);
        }

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, endPoint);

        gameObject.GetComponent<Cable>().DrawMesh();
    }
}
