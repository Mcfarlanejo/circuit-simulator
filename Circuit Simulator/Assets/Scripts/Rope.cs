using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject anchorStart;
    public GameObject anchorEnd;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        anchorEnd.transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += new Vector3(0,-0.1f, 0);
        //gameObject.transform.localScale += new Vector3(0, 0.1f, 0);
    }
}
