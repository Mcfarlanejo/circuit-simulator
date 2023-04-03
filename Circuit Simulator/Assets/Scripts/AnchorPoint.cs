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
    public List<Cable> attachedCables;
    public bool powerSource = false;
    public bool transferPower = false;


    public float volts;
    public float amps;

    private void Awake()
    {
        attachedCables = new List<Cable>();
    }
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
        if (attachedComponent != null)
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

    private void FixedUpdate()
    {
        if ((volts > 0) && (amps > 0))
        {
            transferPower = true;
        }
        else
        {
            transferPower = false;
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
