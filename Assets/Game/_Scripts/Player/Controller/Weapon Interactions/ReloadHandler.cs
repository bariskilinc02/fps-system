using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class ReloadHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
    [SerializeField] private BipedIK bipedIK;

    [SerializeField] private AnimatorOverrideController overrideController;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void Reload()
    {
        StartCoroutine(ReloadHandle_Routine());
    }

    private IEnumerator ReloadHandle_Routine()
    {
        StartCoroutine(IKConfig_Routine(1,0));
        //fullBodyBipedIK.solver.leftHandEffector.positionWeight = 0;
        //bipedIK.solvers.rightHand.IKPositionWeight = 0;
        animator.runtimeAnimatorController = overrideController;
        
        animator.SetBool("Reload", true);
        yield return new WaitForSeconds(3.5f);
        animator.SetBool("Reload", false);
        
        StartCoroutine(IKConfig_Routine(0,1));
        //bipedIK.solvers.rightHand.IKPositionWeight = 1;
        //fullBodyBipedIK.solver.leftHandEffector.positionWeight = 1;

        Debug.Log(overrideController.animationClips.Length);
    }
    
    private IEnumerator IKConfig_Routine(float startValue, float targetValue)
    {
        fullBodyBipedIK.solver.leftHandEffector.positionWeight = startValue;
        bipedIK.solvers.rightHand.IKPositionWeight = startValue;
        
        float time = 0;
        float maxTime = 0.2f;
        while(time < maxTime){
    
            fullBodyBipedIK.solver.leftHandEffector.positionWeight = Mathf.Lerp(startValue,targetValue, time/maxTime);
            bipedIK.solvers.rightHand.IKPositionWeight =Mathf.Lerp(startValue,targetValue, time/maxTime);
            time += Time.deltaTime;
            yield return null;
        }
        
        fullBodyBipedIK.solver.leftHandEffector.positionWeight = targetValue;
        bipedIK.solvers.rightHand.IKPositionWeight = targetValue;
    }
}
