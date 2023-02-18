using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance;

    protected virtual void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = (T)this;
        }
       
    }
}
