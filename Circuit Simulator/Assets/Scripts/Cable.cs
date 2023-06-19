using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public float volts;
    public float amps;

    public AnchorPoint[] anchorPoints;

    private LineRenderer lineRenderer;
    private MeshCollider collider;
    private Mesh mesh;

    private void Awake()
    {
        anchorPoints = new AnchorPoint[2];
    }

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        collider = gameObject.AddComponent<MeshCollider>();

        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            anchorPoint.attachedCables.Add(gameObject.GetComponent<Cable>());
        }
        mesh = new Mesh();
    }

    public void Cascade()
    {
        
    }

    private bool CheckForOtherPowerSource(Cable cable)
    {
        if (cable.gameObject != gameObject && cable.volts != 0)
        {
            return true;
        }
        return false;
    }

    public void DrawMesh()
    {
        lineRenderer.BakeMesh(mesh, true);
        collider.sharedMesh = mesh;
    }
}