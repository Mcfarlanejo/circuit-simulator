using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    public float radius;
    public Color defaultColour;
    public Color highlightedColour;
    public GameObject attachedComponent;
    public bool parentComponentSelected = false;
    // Start is called before the first frame update
    void Update()
    {
        if (parentComponentSelected)
        {
            Highlight();
        }
        else
        {
            SetDefaultColour();
        }
    }

    private void Highlight()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = highlightedColour;
    }

    private void SetDefaultColour()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = defaultColour;
    }
}
