using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelHandler : MonoBehaviour
{
    public List<BarrelPart> barrelParts;

    public BarrelPart currentBarrel;
    private void Awake()
    {
        GetAllBarrelParts();
    }

    public void GetAllBarrelParts()
    {
        barrelParts.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            barrelParts.Add(transform.GetChild(i).GetComponent<BarrelPart>());
        }

    }

    public void CustomizeBarrelPart(int i)
    {
        for (int j = 0; j < barrelParts.Count; j++)
        {
            if (j == i)
            {
                barrelParts[j].gameObject.SetActive(true);
            }
            else
            {
                barrelParts[j].gameObject.SetActive(false);
            }
        }
    }

    public BarrelPart GetActiveBarrelPart()
    {
        for (int i = 0; i < barrelParts.Count; i++)
        {
            if (barrelParts[i].gameObject.activeSelf)
            {
                currentBarrel = barrelParts[i];
                break;
            }
        
        }
        
        return currentBarrel;
    }
}
