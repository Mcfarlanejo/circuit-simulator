using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation = UnityEngine.Animation;
using Material = UnityEngine.Material;

public class LightBlub : Component
{
    public GameObject parent;
    private Animation animation;
    public bool screwedIn = true;
    public LightSwitch lightSwitch;
    public GameObject lightBulb;
    public Material onMaterial;
    public Material offMaterial;
    public GameObject connectedLight;
    private Renderer lightBulbRenderer;


    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();
        lightBulbRenderer = lightBulb.GetComponent<Renderer>();
        componentName = "Light Bulb";
        animation = parent.GetComponent<Animation>();
        lightSwitch.CheckForPower();
    }

    new private void Update()
    {
        base.Update();
    }
    private void OnMouseDown()
    {
        if (screwedIn)
        {
            screwedIn= false;
            UpdateMaterial(false);
            animation.Play("unscrew");
        }
        else
        {
            animation.Play("screwIn");
        }
    }

    public void UpdateMaterial(bool isOn)
    {
        if (isOn)
        {
            lightBulbRenderer.material = onMaterial;
            connectedLight.SetActive(true);
        }
        else
        {
            lightBulbRenderer.material = offMaterial;
            connectedLight.SetActive(false);
        }
    }

    public override void Interact()
    {
        
    }
}
