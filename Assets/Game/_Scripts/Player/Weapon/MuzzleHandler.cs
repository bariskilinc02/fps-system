using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleHandler : MonoBehaviour
{
    public List<MuzzlePart> muzzleParts;

    public MuzzlePart currentMuzzle;

    private void Awake()
    {
        GetAllMuzzleParts();
    }
    
    public void GetAllMuzzleParts()
    {
        muzzleParts.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            muzzleParts.Add(transform.GetChild(i).GetComponent<MuzzlePart>());
        }

    }

    public void CustomizeMuzzlePart(int i)
    {
        for (int j = 0; j < muzzleParts.Count; j++)
        {
            if (j == i)
            {
                muzzleParts[j].gameObject.SetActive(true);
            }
            else
            {
                muzzleParts[j].gameObject.SetActive(false);
            }
        }
    }

    public MuzzlePart GetCurrentMuzzlePart()
    {
        for (int i = 0; i < muzzleParts.Count; i++)
        {
            if (muzzleParts[i].gameObject.activeSelf)
            {
                currentMuzzle = muzzleParts[i];
                break;
            }
        
        }
        
        return currentMuzzle;
    }
    
}
