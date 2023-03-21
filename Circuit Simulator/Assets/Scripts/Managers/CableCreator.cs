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

    private LineRenderer line;
    private bool drawing = false;

    private Vector3 startPos;
    private Vector3 endPos;

    Ray ray;
    RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        
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
                    if (!drawing)
                    {
                        startPos = raycastHit.transform.position;
                        drawing = true;
                    }
                    else
                    {
                        endPos = raycastHit.transform.position;
                        DrawCable();
                        drawing = false;
                    }
                }
            }
        }
    }

    private void DrawCable()
    {
        Vector3 midpoint = (startPos - endPos) / 2;
        //Instantiate(cablePrefab, midpoint, Quaternion.identity);
        cableObjects.Add(Instantiate(cablePrefab));
        line = cableObjects[cableObjects.Count-1].GetComponent<LineRenderer>();

        Vector3[] points = {
        startPos,
        endPos
        };
        line.positionCount = points.Length;
        line.SetPositions(points);

        line.sharedMaterial.color = Color.red;
    }
}
