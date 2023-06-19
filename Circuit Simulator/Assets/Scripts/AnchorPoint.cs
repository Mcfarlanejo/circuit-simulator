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

    //Mouseover colour change vars
    public Color m_MouseOverColor = Color.red;
    public Color m_drawColour = Color.green;
    public MeshRenderer m_Renderer;

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
        SetDefaultColour();
        m_Renderer = GetComponent<MeshRenderer>(); //Get our renderer for this anchor point
    }
    // Start is called before the first frame update
    void Update()
    {
        if ((attachedCables.Count) == 0 && (!powerSource))
        {
            volts = 0;
            amps = 0;
        }
        if (attachedComponent != null)
        {
            if (powerSource)
            {
                volts = attachedComponent.GetComponent<Component>().volts;
                amps = attachedComponent.GetComponent<Component>().amps;
            }
            if (powerSource && volts > 0)
            {
                foreach (Cable cable in attachedCables)
                {
                    foreach (AnchorPoint anchorPoint in cable.anchorPoints)
                    {
                        if (!anchorPoint.powerSource)
                        {
                            anchorPoint.volts = volts;
                            anchorPoint.amps = amps;
                        }
                    }
                }
            }
            else
            {
                foreach (Cable cable in attachedCables)
                {
                    foreach (AnchorPoint anchorPoint in cable.anchorPoints)
                    {
                        if (!anchorPoint.powerSource && !CheckForPowerSource(anchorPoint.attachedCables))
                        {
                            anchorPoint.volts = 0;
                            anchorPoint.amps = 0;
                        }
                    }
                }
            }
            if (!powerSource)
            {
                attachedComponent.GetComponent<Component>().volts = volts;
                attachedComponent.GetComponent<Component>().amps = amps;
            }
        }
    }

    void OnMouseEnter() //Change colour to red on mouse over, unless we are Green (Green indicates the anchor node is highlighted for cable drawing)
    {
        if (m_Renderer.material.color != m_drawColour)
        {
            m_Renderer.material.color = m_MouseOverColor;
        }
    }

    void OnMouseExit() //Change back to blue when mouse leaves, as long as we are not Green
    {
        if (m_Renderer.material.color != m_drawColour)
        {
            m_Renderer.material.color = defaultColour;
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

    private bool CheckForPowerSource(List<Cable> cables)
    {
        foreach (Cable cable in cables)
        {
            foreach (AnchorPoint anchorPoint in cable.anchorPoints)
            {
                if (anchorPoint.powerSource && anchorPoint.volts > 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
