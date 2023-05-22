using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public Camera overlay;
    public bool front = true;
    private Vector3 frontPosition;
    private Vector3 backPosition;
    
    private Vector3 lerpPos; //Lerp Target
    private Quaternion lerpRot; //Lerp rotation target
    private float curLerpTime; //This and next are for smoothing time values.
    private float lerpTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize front position, set targets appropriately.
        frontPosition = cam.transform.position;
        backPosition = new Vector3(frontPosition.x, frontPosition.y, -frontPosition.z + 1);
        lerpPos = frontPosition;
        lerpRot = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        curLerpTime += Time.deltaTime;
        if (curLerpTime > lerpTime)
        {
            curLerpTime = lerpTime;
        }
        float perc = curLerpTime / lerpTime; //Calculate value through lerp based on time passed.

        //Push camera towards target position through a Lerp.
        cam.transform.position = Vector3.Lerp(cam.transform.position, lerpPos, perc);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, lerpRot, perc);
        
        if (Input.GetKeyDown("tab")) //Switch target positions.
        {
            curLerpTime = 0f; //Reset Lerp timer.
            if (front)
            {

                lerpPos = backPosition;
                lerpRot = Quaternion.Euler(0, 180, 0);
                overlay.gameObject.SetActive(true);
                front = false;
            }
            else
            {
                lerpPos = frontPosition;
                lerpRot = Quaternion.Euler(0, 0, 0);
                overlay.gameObject.SetActive(false);
                front = true;
            }

        }
    }
}
