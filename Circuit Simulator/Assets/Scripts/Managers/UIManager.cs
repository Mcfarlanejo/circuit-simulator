using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject flip;
    private Image tab;
    public Sprite unpressed;
    public Sprite pressed;
    private bool front;

    void Start()
    {
        tab = flip.transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            if (!front)
            {
                tab.sprite = pressed;
            }
            else
            {
                tab.sprite = unpressed;
            }
            front = !front;
        }
    }
}
