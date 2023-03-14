using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentButton : MonoBehaviour
{
    public Button button;
    public int index;

    private void Awake()
    {
        button.onClick.AddListener(OnClickedButton);
    }

    public virtual void OnClickedButton()
    {
      
    }
}
