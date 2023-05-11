using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera cam;
    public bool front = true;
    private Vector3 frontPosition;
    private Vector3 backPosition;

    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

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
        frontPosition = cam.transform.position;
        backPosition = new Vector3(frontPosition.x, frontPosition.y, -frontPosition.z + 1);
        cam.transform.rotation = Quaternion.identity;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
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

        if (front)
        {
            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.y += Input.GetAxis(yAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            cam.transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
        }
    }
}
