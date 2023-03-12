using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripHandler : MonoBehaviour
{
    public List<GripPart> gripParts;

    public GripPart currentGrip;
    private void Awake()
    {
        GetAllGripParts();

    }

    public void GetAllGripParts()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            gripParts.Add(transform.GetChild(i).GetComponent<GripPart>());
        }

    }

    public GripPart GetCurrentGripPart()
    {
        for (int i = 0; i < gripParts.Count; i++)
        {
            if (gripParts[i].gameObject.activeSelf)
            {
                currentGrip = gripParts[i];
                break;
            }
        
        }
        
        return currentGrip;
    }
}
