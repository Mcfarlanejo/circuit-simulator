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

    public GameObject popUpPrefab;
    public GameObject popUpContainer;

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
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(popUpContainer);

            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    DisplayInfo(raycastHit.transform.gameObject);
                }
            }
        }
    }

    private void DisplayInfo(GameObject gameObject)
    {
        popUpContainer = Instantiate(popUpPrefab, GameObject.Find("Canvas").transform);
        Text voltText = GameObject.Find("VoltsValue").GetComponent<Text>();
        Text faultValue = GameObject.Find("FaultValue").GetComponent<Text>();
        if (fault)
        {
            popUpContainer.GetComponent<Image>().color = faultColour;
            faultValue.text = "Yes";
        }
        else
        {
            popUpContainer.GetComponent<Image>().color = defaultColour;
            faultValue.text = "No";
        }
        voltText.text = Convert.ToString(volts);
    }
}
