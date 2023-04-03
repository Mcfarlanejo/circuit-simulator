using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Component : MonoBehaviour
{
    public bool fault = false;

    public const float STANDARDVOLTAGE = 230f;
    public const float STANDARDAMPS = 10f;

    public float inputVolts;
    public float volts;
    public float amps;
    public float outputVolts;

    public float expectedVoltage = STANDARDVOLTAGE;
    public float expectedAmps = STANDARDAMPS;

    public Color defaultColour = Color.blue;
    public Color faultColour = Color.red;

    public bool powerSource = false;

    public GameObject popUp;

    public List<AnchorPoint> anchorPoints;

    private Text voltText;
    private Text faultValue;
    private Text ampsText;

    Ray ray;
    RaycastHit raycastHit;     
    
    // Start is called before the first frame update
    void Start()
    {
        AssignValues();

        inputVolts = volts;
        outputVolts = volts;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFault();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if ((raycastHit.transform != null) && (raycastHit.collider.name == gameObject.name))
                {
                    Interact();
                    CheckForFault();
                    DisplayInfo(raycastHit.transform.gameObject);
                    //if (anchorPoints[0].parentComponentSelected == false)
                    //{
                    //    ToggleAnchorPoints();
                    //}
                }
            }
            else
            {
                popUp.SetActive(false);
                //if (anchorPoints[0].parentComponentSelected == true)
                //{
                //    //ToggleAnchorPoints();
                //}
            }
        }
    }

    private void CheckForFault()
    {
        if ((volts != expectedVoltage) || (amps != expectedAmps))
        {
            fault = true;
        }
        else if ((volts == expectedVoltage) && (amps == expectedAmps))
        {
            fault = false;
        }
    }

    private void DisplayInfo(GameObject gameObject)
    {
        popUp.SetActive(true);
        voltText = GameObject.Find("VoltsValue").GetComponent<Text>();
        faultValue = GameObject.Find("FaultValue").GetComponent<Text>();
        ampsText = GameObject.Find("AmpsValue").GetComponent<Text>();
        if (fault)
        {
            popUp.GetComponent<Image>().color = faultColour;
            faultValue.text = "Yes";
        }
        else
        {
            popUp.GetComponent<Image>().color = defaultColour;
            faultValue.text = "No";
        }
        voltText.text = Convert.ToString(volts) + "V";
        ampsText.text = Convert.ToString(amps) + "A";
    }

    private void ToggleAnchorPoints()
    {
        foreach (AnchorPoint point in anchorPoints)
        {
            point.parentComponentSelected = !point.parentComponentSelected;
        }
    }

    public void AssignValues()
    {
        volts = STANDARDVOLTAGE;
        amps= STANDARDAMPS;
    }

    public abstract void Interact();
}
