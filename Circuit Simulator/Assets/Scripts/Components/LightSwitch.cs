using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : Component
{
    public float onAngle;
    public float offAngle;
    public GameObject switchObject;
    public bool switchOn = false;
    private Quaternion target;
    public GameObject connectedLight;

    public GameObject lightBulb; 
    public Material onMaterial;
    public Material offMaterial;

    private Renderer lightBulbRenderer;

    private void Start()
    {
        lightBulbRenderer = lightBulb.GetComponent<Renderer>();
        CheckStartPosition();
        
        switchObject.transform.rotation = target;
        UpdateMaterial();
    }

    private void FixedUpdate()
    {
        if (switchOn && !fault)
        {
            connectedLight.SetActive(true);
        }
        else
        {
            connectedLight.SetActive(false);
        }
        UpdateMaterial();
    }
    public override void Interact()
    {
        if (!switchOn)
        {
            target = Quaternion.Euler(onAngle, 0, 0);
            SwitchTriggered();
        }
        else
        {
            target = Quaternion.Euler(-offAngle, 0, 0);
            SwitchTriggered();
        }
        switchObject.transform.rotation = target;
         
    }

    void CheckStartPosition()
    {
        switchOn = volts == expectedVoltage ? true : false;
        target = connectedLight.activeInHierarchy ? Quaternion.Euler(onAngle, 0, 0) : Quaternion.Euler(-offAngle, 0, 0);
        connectedLight.SetActive(switchOn);
    }

    void SwitchTriggered()
    {   
        switchOn = !switchOn;
    }

    void UpdateMaterial()   
    {
        if (switchOn && !fault)
        {
            lightBulbRenderer.material = onMaterial;
        }
        else
        {
            lightBulbRenderer.material = offMaterial;
        }
    }
}
