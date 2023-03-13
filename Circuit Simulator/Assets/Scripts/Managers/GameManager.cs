using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cablePrefab;
    private bool drawing = false;
    private bool drawReady = false;
    private Vector3 cableStartPoint;
    private Vector3 cableEndPoint;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    if (!drawing)
                    {
                        cableStartPoint = raycastHit.transform.position;
                        drawing = true;
                    }
                    else
                    {
                        cableEndPoint = raycastHit.transform.position;
                        drawing = false;
                        drawReady = true;
                    }
                }
            }
        }

        if (drawReady)
        {
            Vector3 distanceBetween = (cableStartPoint + cableEndPoint) / 2;
            Instantiate(cablePrefab, distanceBetween, Quaternion.Euler(90,90,0));
            
            drawReady = false;
        }
    }
}
