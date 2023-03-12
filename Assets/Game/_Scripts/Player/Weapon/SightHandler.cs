using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SightHandler : MonoBehaviour
{
    
    public List<SightPart> sigthParts;

    private SightPart currentSightPart;
    
    private void Awake()
    {
        GetAllSightParts();

    }
    
    public void GetAllSightParts()
    {
        sigthParts.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            sigthParts.Add(transform.GetChild(i).GetComponent<SightPart>());
        }

    }
    
    public void CustomizeSightPart(int i)
    {
        for (int j = 0; j < sigthParts.Count; j++)
        {
            if (j == i)
            {
                sigthParts[j].gameObject.SetActive(true);
            }
            else
            {
                sigthParts[j].gameObject.SetActive(false);
            }
        }
    }
    
    public SightPart GetCurrentSigthPart()
    {
        for (int i = 0; i < sigthParts.Count; i++)
        {
            if (sigthParts[i].gameObject.activeSelf)
            {
                currentSightPart = sigthParts[i];
                break;
            }
        
        }
        
        return currentSightPart;
    }

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
