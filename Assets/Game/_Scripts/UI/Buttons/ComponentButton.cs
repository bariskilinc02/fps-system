using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponentButton : MonoBehaviour
{
    public Button button;
    public int index;
    public TextMeshProUGUI text;
    
    private void Awake()
    {
        text.text = $"{transform.parent.parent.parent.name} {index}";
        button.onClick.AddListener(OnClickedButton);
    }

    public virtual void OnClickedButton()
    {
      
    }
}
