using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject tab;
    private GameObject flip;
    public Sprite unpressed;
    public Sprite pressed;
    private bool front;
    
    private Quaternion lerpRot; //Lerp rotation target
    private float curLerpTime; //This and next are for smoothing time values.
    private float lerpTime = 1f;

    void Start()
    {
        flip = tab.transform.GetChild(0).gameObject;
        lerpRot = Quaternion.Euler(0, 0, 0);
    }
    
    void Update()
    {
        curLerpTime += Time.deltaTime;
        if (curLerpTime > lerpTime)
        {
            curLerpTime = lerpTime;
        }
        float perc = curLerpTime / lerpTime;

        flip.transform.rotation = Quaternion.Lerp(flip.transform.rotation, lerpRot, perc);

        if (Input.GetKeyDown("tab"))
        {
            if (!front)
            {
                tab.GetComponent<Image>().sprite = pressed;
                lerpRot = Quaternion.Euler(0, 0, 180);
            }
            else
            {
                tab.GetComponent<Image>().sprite = unpressed;
                lerpRot = Quaternion.Euler(0, 0, 0);
            }
            front = !front;
            curLerpTime = 0f;
        }
    }
}
