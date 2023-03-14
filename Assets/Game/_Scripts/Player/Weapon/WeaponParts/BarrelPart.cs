using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelPart : MonoBehaviour
{
    public GripHandler currentGripHandler;
    public MuzzleHandler currentMuzzleHandler;
    public GripHandler ReturnActiveGripHandler()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                if (transform.GetChild(i).GetComponent<GripHandler>() != null)
                {
                    currentGripHandler = transform.GetChild(i).GetComponent<GripHandler>();
                    break;
                }
            }
        
        }
        
        return currentGripHandler;
        
    }
    
    public MuzzleHandler ReturnActiveMuzzleHandler()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                if (transform.GetChild(i).GetComponent<MuzzleHandler>() != null)
                {
                    currentMuzzleHandler = transform.GetChild(i).GetComponent<MuzzleHandler>();
                    break;
                }
            }
        
        }
        
        return currentMuzzleHandler;
        
    }
}
