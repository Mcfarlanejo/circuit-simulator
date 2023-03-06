using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Component : MonoBehaviour
{
    public bool fault = false;
    public float volts = 0;
    public float faultTollerence = 5f;
    public Color defaultColour = Color.blue;
    public Color faultColour = Color.red;

    public GameObject popUp;

    private Text voltText;
    private Text faultValue;

    Ray ray;
    RaycastHit raycastHit;     
    
    // Start is called before the first frame update
    void Start()
    {
        volts = UnityEngine.Random.Range(0f, 10f);
        if (volts > faultTollerence)
        {
            fault = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if ((raycastHit.transform != null) && (raycastHit.collider.name == gameObject.name))
                {
                    DisplayInfo(raycastHit.transform.gameObject);
                }
            }
            else
            {
                popUp.SetActive(false);
            }
        }
    }

    private void DisplayInfo(GameObject gameObject)
    {
        popUp.SetActive(true);
        voltText = GameObject.Find("VoltsValue").GetComponent<Text>();
        faultValue = GameObject.Find("FaultValue").GetComponent<Text>();
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
        voltText.text = Convert.ToString(volts);
    }
}
