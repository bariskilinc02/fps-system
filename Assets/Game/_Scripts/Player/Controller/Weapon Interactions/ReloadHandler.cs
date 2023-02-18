using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadHandler : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        StartCoroutine(Reload_Routine());
    }
    public IEnumerator Reload_Routine()
    {
        yield return null;
    }
}
