using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RootMotion.FinalIK;
using UnityEngine;

public class ReloadHandler : MonoBehaviour
{
     [SerializeField] private PlayerManager playerManager;
     
     private Animator animator;
     private FullBodyBipedIK fullBodyBipedIK;
     private BipedIK bipedIK;
    private WeaponHandler weaponHandler;
    private AimIK aimIK;

    [SerializeField] private AnimatorOverrideController overrideController;


    public Transform aimTransform;
    public Transform handTransform;

    private void Awake()
    {
        animator = playerManager.animator;
        fullBodyBipedIK = playerManager.fullBodyBipedIK;
        bipedIK = playerManager.bipedIK;
        weaponHandler = playerManager.weaponHandler;
        aimIK = playerManager.aimIK;
    }

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
        weaponHandler.onAim = false;
        float duration = weaponHandler.currentWeapon.weaponOverrider.animationClips[3].length;

        animator.runtimeAnimatorController = weaponHandler.currentWeapon.weaponOverrider;
        animator.runtimeAnimatorController = weaponHandler.currentWeapon.currentGrip.gridOverrider;
        StartCoroutine(IKConfig_Routine(1,0));
        animator.SetBool("Reload", true);
        
        yield return new WaitForSeconds(duration - 1);
        
        animator.SetBool("Reload", false);
        yield return new WaitForSeconds(0.8f);
      
        StartCoroutine(IKConfig_Routine(0,1));
       
    }
    
    private IEnumerator IKConfig_Routine(float startValue, float targetValue)
    {
        fullBodyBipedIK.solver.leftHandEffector.positionWeight = startValue;
        bipedIK.solvers.rightHand.IKPositionWeight = startValue;
        animator.SetLayerWeight(7, startValue);
        float time = 0;
        float maxTime = 0.25f;
        while(time < maxTime){
    
            fullBodyBipedIK.solver.leftHandEffector.positionWeight = Mathf.Lerp(startValue,targetValue, time/maxTime);
            bipedIK.solvers.rightHand.IKPositionWeight = Mathf.Lerp(startValue,targetValue, time/maxTime);
            animator.SetLayerWeight(7, Mathf.Lerp(startValue,targetValue, time/maxTime));
            time += Time.deltaTime;
            yield return null;
        }
        
        fullBodyBipedIK.solver.leftHandEffector.positionWeight = targetValue;
        bipedIK.solvers.rightHand.IKPositionWeight = targetValue;
        animator.SetLayerWeight(7, targetValue);
    }
}
