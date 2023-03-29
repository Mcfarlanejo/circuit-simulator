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
    public Cable attachedCable;
    public bool powerSource = false;

    public float volts;
    public float amps;

    private void Start()
    {
        if (attachedComponent.GetComponent<Component>().powerSource)
        {
            powerSource = true;
        }
    }
    // Start is called before the first frame update
    void Update()
    {
        if (powerSource)
        {
            volts = attachedComponent.GetComponent<Component>().volts;
            amps = attachedComponent.GetComponent<Component>().amps;
        }
        else
        {
            attachedComponent.GetComponent<Component>().volts = volts;
            attachedComponent.GetComponent<Component>().amps = amps;
        }
        
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
