using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CableCreator : MonoBehaviour
{
    public GameObject cable;
    public Camera cam;
    private GameObject currentCable;
    private Vector3[] currentCablePoints;
    private bool drawing = false;

    private Vector3 startPos;
    private Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !drawing)
        {
            drawing = true;
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = cam.ScreenToViewportPoint(mousePos);
            startPos = objectPos;
            currentCable = Instantiate(cable, objectPos, Quaternion.identity);
            if (Input.GetMouseButtonUp(0))
            {
                Vector3 mousePosEnd = Input.mousePosition;
                Vector3 objectPosEnd = Camera.current.ScreenToViewportPoint(mousePos);
                endPos = objectPosEnd;

                LineRenderer currentCableLR = currentCable.gameObject.GetComponent<LineRenderer>();
                currentCableLR.positionCount = Convert.ToInt32(CalculateCableLength());
                currentCableLR.GetPositions(currentCablePoints);

                Debug.Log(currentCableLR.positionCount);
            }
            drawing = false;
        }
    }

    private float CalculateCableLength()
    {
        float cableLength;
        cableLength = Vector3.Distance(startPos, endPos);
        Debug.Log(cableLength);
        return cableLength;
    }
}
