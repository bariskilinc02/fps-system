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
            //var forward = Vector3.forward;
            //var angle = Vector3.Angle(GameManager.Instance.RightHandTarget.forward, forward);
            //Debug.Log(angle);
            aimPosition = GameManager.Instance.RightHandTarget.position - calculatorTransform.position;
            Debug.Log( aimPosition.magnitude);
                
            //Vector3 rotatedAimVector = Quaternion.AngleAxis(45, Vector3.forward) * aimPosition;
            // Debug.Log(rotatedAimVector);
            //aimPosition = rotatedAimVector;
        }
        
    }
}
