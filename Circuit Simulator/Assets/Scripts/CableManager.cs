using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private Vector3[] points;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
