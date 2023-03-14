using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripPart : MonoBehaviour
{
    public AnimatorOverrideController gridOverrider;
    public Transform leftHandTarget;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "LeftHandTarget")
            {
                leftHandTarget = transform.GetChild(i);
                break;
            }
        }
 
    }

    public void SetAnimatorOverrider(Animator animator)
    {
        animator.runtimeAnimatorController = gridOverrider;
    }

    public AnimatorOverrideController GetOverrider()
    {
        return gridOverrider;
    }
}
