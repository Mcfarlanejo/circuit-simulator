using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbParent : MonoBehaviour
{
    public LightBlub lightBlub;

    public void TurnLightOn()
    {
        lightBlub.UpdateMaterial(true);
        
        lightBlub.screwedIn = true;
    }
}
