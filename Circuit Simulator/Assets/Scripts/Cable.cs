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

    private void Update()
    {
        if ((anchorPoints[0].gameObject.name != "-AnchorPoint") && (anchorPoints[1].gameObject.name != "-AnchorPoint") &&
            (anchorPoints[0].gameObject.name != "+AnchorPoint") && (anchorPoints[1].gameObject.name != "+AnchorPoint"))
        {
            if (((anchorPoints[0].powerSource) && (anchorPoints[0].volts != 0)) ||
                ((anchorPoints[1].powerSource) && (anchorPoints[1].volts != 0)))
            {
                foreach (AnchorPoint anchorPoint in anchorPoints)
                {
                    if (anchorPoint.powerSource)
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
            else if (((anchorPoints[0].transferPower) && (anchorPoints[0].volts != 0)) ||
                    ((anchorPoints[1].transferPower) && (anchorPoints[1].volts != 0)))
            {
                foreach (AnchorPoint anchorPoint in anchorPoints)
                {
                    if (anchorPoint.transferPower)
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
            else if (anchorPoints[0].volts == 0 && anchorPoints[1].volts == 0)
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

    public void Cascade()
    {
        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            if (!anchorPoint.powerSource)
            {
                foreach (Cable cable in anchorPoint.attachedCables)
                {
                    if (!CheckForOtherPowerSource(cable))
                    {
                        anchorPoint.transferPower = false;
                        anchorPoint.volts = 0;
                        anchorPoint.amps = 0;
                    }
                }
            }
        }
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