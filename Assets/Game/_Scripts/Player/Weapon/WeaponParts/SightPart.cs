using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightPart : MonoBehaviour
{
    private Transform calculatorTransform;
    public Vector3 aimPosition;

    [SerializeField] private bool OnCalculate;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "calculator")
            {
                calculatorTransform = child;
            }
        }

        if (!OnCalculate)
        {
            calculatorTransform.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (OnCalculate)
        {
            aimPosition = GameManager.Instance.RightHandTarget.position - calculatorTransform.position;
        }
        
    }
}
