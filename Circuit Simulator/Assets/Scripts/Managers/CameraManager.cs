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

    [SerializeField] UIManager ui; //Reference for UI

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits horizontal camera rotation. Prevents the camera from turning too far.")]
    [Range(0f, 90f)][SerializeField] float xRotationLimit = 15f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 15f;

    Vector2 rotation = Vector2.zero;
    const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    const string yAxis = "Mouse Y";


    public int boundary = 50;
    public int speed = 5;

    private int screenWidth;
    private int screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize front position, set targets appropriately.
        frontPosition = cam.transform.position;
        backPosition = new Vector3(frontPosition.x, frontPosition.y, -frontPosition.z + 1);
        lerpPos = frontPosition;
        lerpRot = Quaternion.Euler(0, 0, 0);
        screenWidth = Screen.width;
        screenHeight = Screen.height;
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
            Flip();
        }

        if (front)
        {
            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -xRotationLimit, xRotationLimit);
            rotation.y += Input.GetAxis(yAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            cam.transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }
    }

    public void Flip()
    {
        ui.Flip();
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
