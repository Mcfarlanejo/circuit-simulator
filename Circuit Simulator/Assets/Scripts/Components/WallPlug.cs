using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlug : MonoBehaviour
{
    public Material hoverMaterial;
    public Material defaultMaterial;
    // Start is called before the first frame update
    void Start()
    {
        defaultMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<MeshRenderer>().material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
    }
}
