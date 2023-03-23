using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CableCreator : MonoBehaviour
{
    public GameObject cablePrefab;
    public List<GameObject> cableObjects;
    
    private Color defaultCableColour = Color.yellow;
    private Color positiveCableColour = Color.red;
    private Color negativeCableColour = Color.black;
    
    public Color cableColour;

    private LineRenderer line;
    private bool drawing = false;

    private Vector3 startPos;
    //private AnchorPoint startAnchor;
    private Vector3 endPos;
    //private AnchorPoint endAnchor;

    Ray ray;
    RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        cableColour = defaultCableColour;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if ((raycastHit.transform != null) && (raycastHit.transform.GetComponent<AnchorPoint>() != null))
                {
                    Debug.Log($"Hit {raycastHit.collider.name} at {raycastHit.transform.position}");
                    if (raycastHit.collider.name == "+AnchorPoint")
                    {
                        cableColour = positiveCableColour;
                    }
                    else if (raycastHit.collider.name == "-AnchorPoint")
                    {
                        cableColour = negativeCableColour;
                    }

                    if (!drawing)
                    {
                        startPos = raycastHit.transform.position;
                        //startAnchor = raycastHit.transform.GetComponent<AnchorPoint>();
                        drawing = true;
                    }
                    else
                    {
                        endPos = raycastHit.transform.position;
                        //endAnchor = raycastHit.transform.GetComponent<AnchorPoint>();
                        DrawCable();
                        drawing = false;
                    }
                }
            }
        }
    }

    private void DrawCable()
    {
        cableObjects.Add(Instantiate(cablePrefab));
        GameObject lineObject = cableObjects[cableObjects.Count - 1];
        //lineObject.GetComponent<Cable>().anchorPoints[0] = startAnchor;
        //lineObject.GetComponent<Cable>().anchorPoints[1] = endAnchor;
        line = lineObject.GetComponent<LineRenderer>();

        Vector3[] points = {
        startPos,
        endPos
        };

        line.positionCount = points.Length;
        line.SetPositions(points);
        line.sharedMaterial.color = cableColour;

        cableColour = defaultCableColour;
    }
}
