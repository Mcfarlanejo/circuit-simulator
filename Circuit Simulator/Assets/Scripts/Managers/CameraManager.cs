using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public bool front = true;
    private Vector3 frontPosition;
    private Vector3 backPosition;

    // Start is called before the first frame update
    void Start()
    {
        frontPosition = cam.transform.position;
        backPosition = new Vector3(frontPosition.x, frontPosition.y, -frontPosition.z + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (front)
            {
                cam.transform.position = backPosition;
                cam.transform.rotation = Quaternion.Euler(0, 180, 0);
                front = false;
            }
            else
            {
                cam.transform.position = frontPosition;
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                front = true;
            }

        }
    }
}
