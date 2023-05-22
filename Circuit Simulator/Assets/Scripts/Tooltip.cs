using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public Camera cam;
    private Vector3 min, max;
    private RectTransform rect;
    private float offset = 3f;

    public string name, volts, amps, fault;
    public TMP_Text namebox, vbox, abox, fbox;
    private string namePre = "Name: ";
    private string voltPre = "Voltage: ";
    private string ampPre = "Amperage: ";
    private string faultPre = "Fault: ";

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        min = new Vector3(0, 0, 0);
        max = new Vector3(cam.pixelWidth, cam.pixelHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ClampBounds();
        UpdateTooltip();
    }

    private void ClampBounds()
    {
        if (gameObject.activeSelf)
        {
            //get the tooltip position with offset
            Vector3 position = new Vector3(Input.mousePosition.x + rect.rect.width, Input.mousePosition.y - (rect.rect.height / 2 + offset), 0f);
            //clamp it to the screen size so it doesn't go outside
            transform.position = new Vector3(Mathf.Clamp(position.x, min.x + rect.rect.width/2, max.x - rect.rect.width/2), Mathf.Clamp(position.y, min.y + rect.rect.height / 2, max.y - rect.rect.height / 2), transform.position.z);
        }
    }

    private void UpdateTooltip()
    {
        namebox.text = namePre + name;
        vbox.text = voltPre + volts;
        abox.text = ampPre + amps;
        fbox.text = faultPre + fault;
    }
}
