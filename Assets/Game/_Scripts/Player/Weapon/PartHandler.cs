using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PartHandler : MonoBehaviour
{ 
    public List<PartBase> parts;

    public PartBase currentPart;
    private void Awake()
    {
        GetAllParts();
    }

    public void GetAllParts()
    {
        parts.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            parts.Add(transform.GetChild(i).GetComponent<T>());
        }

    }

    public void CustomizePart(int i)
    {
        for (int j = 0; j < parts.Count; j++)
        {
            if (j == i)
            {
                parts[j].gameObject.SetActive(true);
            }
            else
            {
                parts[j].gameObject.SetActive(false);
            }
        }
    }

    public PartBase GetCurrentPart()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i].gameObject.activeSelf)
            {
                currentPart = parts[i];
                break;
            }
        
        }
        
        return currentPart;
    }
}
