using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using TMPro;

public class CircuitBreaker : Component
{
    public Vector3 onPosition;
    public Vector3 offPosition;
    public GameObject switchObject;
    public bool switchOn = false;
    public bool lockout = false;
    private Vector3 target;
    public Button lockoutButton;
    public GameObject lockoutTag;

    private void Awake()
    {
        powerSource = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        lockoutButton.onClick.AddListener(ToggleLockOutTag);
        AssignValues();
        CheckStartPosition();
        switchObject.transform.localPosition = target;
    }
    private void FixedUpdate()
    {
        foreach (AnchorPoint anchorPoint in anchorPoints)
        {
            anchorPoint.volts = volts;
            anchorPoint.amps = amps;
        }
    }

    public override void Interact()
    {
        if (!lockout)
        {
            if (!switchOn)
            {
                target = onPosition;
                volts = STANDARDVOLTAGE;
                amps = STANDARDAMPS;
                switchOn = true;
                lockoutButton.gameObject.SetActive(false);
            }
            else
            {
                target = offPosition;
                volts = 0;
                amps = 0;
                switchOn = false;
                lockoutButton.gameObject.SetActive(true);
            }
        }
        
        switchObject.transform.localPosition = target;
    }

    void CheckStartPosition()
    {
        switchOn = switchObject.transform.localPosition == onPosition ? true : false;
        target = switchOn ? onPosition : offPosition;
    }

    void ToggleLockOutTag()
    {
        lockoutTag.SetActive(!lockoutTag.activeInHierarchy);
        if (lockoutTag.activeInHierarchy)
        {
            lockoutButton.image.color = Color.red;
            lockout = true;
            lockoutButton.GetComponentInChildren<TMP_Text>().text = "Unlock?";
        }
        else
        {
            lockoutButton.image.color = Color.white;
            lockout = false;
            lockoutButton.GetComponentInChildren<TMP_Text>().text = "Lockout?";
        }
        
    }
}
