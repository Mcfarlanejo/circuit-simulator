using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    public float radius;
    public Color defaultColour;
    public Color highlightedColor;
    public GameObject attachedComponent;
    public bool parentComponentSelected = false;
    // Start is called before the first frame update
    void Update()
    {
        //if (parentComponentSelected)
        //{
        //    ShowWireSpheres();
        //}
    }

    //private void ShowWireSpheres()
    //{
    //    Gizmos.color = defaultColour;
    //    Gizmos.DrawSphere(transform.position, radius);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = defaultColour;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
