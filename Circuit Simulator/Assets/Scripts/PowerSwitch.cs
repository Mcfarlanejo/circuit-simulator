using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    // The nested object that will move when the parent object is clicked
    public GameObject nestedObject;

    // The position of the nested object when it's in its original position
    private Vector3 originalPosition;

    // Whether the nested object is currently moved or not
    private bool isMoved = false;

    public Canvas canvas;

    

    void Start()
    {
        // Store the original position of the nested object
        originalPosition = nestedObject.transform.localPosition;

        if (canvas == null)
        {
            canvas = GetComponent<Canvas>();
        }

        // Make sure the canvas is initially active
        canvas.enabled = false;

    }

    void OnMouseDown()
    {
        // Move the nested object if it's not already moved, otherwise return it to its original position
        if (!isMoved)
        {
            nestedObject.transform.localPosition += new Vector3(-0.35f, 0f, 0f);
            isMoved = true;
            canvas.enabled = !canvas.enabled;
        }
        else
        {
            nestedObject.transform.localPosition = originalPosition;
            isMoved = false;
            canvas.enabled = !canvas.enabled;
        }
    }
}
