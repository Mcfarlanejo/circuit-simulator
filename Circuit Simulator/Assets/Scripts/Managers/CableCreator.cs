using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class CableCreator : MonoBehaviour
{
    public GameObject cablePrefab;
    public List<GameObject> cableObjects;
    
    public Material defaultCableColour;
    public Material positiveCableColour;
    public Material negativeCableColour;
    
    private Material cableColour;

    private LineRenderer line;
    private bool drawing = false;

    private Vector3 startPos;
    private AnchorPoint startAnchor;
    private Vector3 endPos;
    private AnchorPoint endAnchor;

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
                if ((raycastHit.transform != null) && (raycastHit.transform.GetComponent<AnchorPoint>() != null) && 
                    (raycastHit.transform.position != startPos))
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
                        SetStartAnchor(raycastHit);
                    }
                    else if (startAnchor.GetComponentInParent<Component>() == null)
                    {
                        SetEndAnchor(raycastHit);
                    }
                    else if ((raycastHit.transform.GetComponentInParent<Component>().gameObject !=
                            startAnchor.GetComponentInParent<Component>().gameObject) && ((raycastHit.collider.name
                            != "+AnchorPoint") || (raycastHit.collider.name != "-AnchorPoint")))
                    {
                        SetEndAnchor(raycastHit);
                    }
                    else
                    {
                        ResetValues();
                    }
                }
            }
        }
        if ((Input.GetMouseButtonDown(2)) && ((Physics.Raycast(ray, out raycastHit, 100f))))
        {
            if ((raycastHit.transform != null) && (raycastHit.transform.GetComponent<Cable>() != null))
            {
                DeleteCable(raycastHit.transform.gameObject);
            }
        }
    }

    private void SetStartAnchor(RaycastHit raycastHit)
    {
        startPos = raycastHit.transform.position;
        startAnchor = raycastHit.transform.GetComponent<AnchorPoint>();
        drawing = true;
    }

    private void SetEndAnchor(RaycastHit raycastHit)
    {
        endPos = raycastHit.transform.position;
        endAnchor = raycastHit.transform.GetComponent<AnchorPoint>();
        if (startAnchor.attachedCables.Count == 0)
        {
            DrawCable();
        }
        else
        {
            foreach (Cable cable in startAnchor.attachedCables)
            {
                if (!(cable.anchorPoints.Contains(startAnchor) && cable.anchorPoints.Contains(endAnchor)))
                {
                    DrawCable();
                }
            }
        }
        
        ResetValues();
    }

    private void DrawCable()
    {
        cableObjects.Add(Instantiate(cablePrefab));
        GameObject lineObject = cableObjects[cableObjects.Count - 1];
        lineObject.GetComponent<Cable>().anchorPoints[0] = startAnchor;
        lineObject.GetComponent<Cable>().anchorPoints[1] = endAnchor;
        line = lineObject.GetComponent<LineRenderer>();

        Vector3[] points = {
        startPos,
        endPos
        };

        line.positionCount = points.Length;
        line.SetPositions(points);
        line.sharedMaterial = cableColour;
    }

    private void ResetValues()
    {
        cableColour = defaultCableColour;

        startAnchor = null;
        startPos = Vector3.zero;
        endAnchor = null;
        endPos = Vector3.zero;

        drawing = false;
    }

    private void DeleteCable(GameObject cable)
    {
        
        Debug.Log("Again?");
        foreach (AnchorPoint anchorPoint in cable.GetComponent<Cable>().anchorPoints)
        {
            Debug.Log(anchorPoint.attachedCables);
            anchorPoint.attachedCables.Remove(cable.GetComponent<Cable>());
            Debug.Log(anchorPoint.attachedCables);
        }
        Destroy(cable);
    }
}
