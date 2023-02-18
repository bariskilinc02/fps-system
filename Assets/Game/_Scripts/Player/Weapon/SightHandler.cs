using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SightHandler : MonoBehaviour
{
    private SightPart currentSightPart;

    public void SetSightPart()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
            {
                currentSightPart = child.GetComponent<SightPart>();
            }
        }
    }

    public SightPart GetSightPart()
    {
        SetSightPart();

        return currentSightPart;
    }

    public float GetAimPositionX()
    {
        return currentSightPart.aimPosition.y;
    }
}
