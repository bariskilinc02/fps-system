using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class SlideHandler : MonoBehaviour
{
    [Header("Sliding")]
    public bool isSliding;

    private float slideTimer;
    private Vector3 rightHandStartRotation;
    private Vector3 rightElbowStartRotation;
    private Vector3 rightShoulderStartRotation;
    private Vector3 rightShoulderCurrentRotation;
    
    
    public Vector3 handSlideVector;
    
    private Vector3 weaponRootRotation;
    private Vector3 currentWeaponRootRotation;
    [SerializeField] private Animator animator;
    [SerializeField] private BipedIK bipedIK;
    [SerializeField] private Transform rightHandBone;
    [SerializeField] private Transform rightShoulderBone;
    [SerializeField] private Transform rightElbowBone;
    [SerializeField] private Transform weaponRoot;

    [SerializeField] private AnimationCurve SlideCurve;
    
    //addditioa
    public AimIK aimIK;
    public FullBodyBipedIK fullBodyBipedIK;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartCoroutine(SlideDownWeapon_RoutineLast());
        }
     
    }

    public void SlideDownWeapon()
    {
        StartCoroutine(SlideDownWeapon_RoutineTrial());
        
    }
    
    public void SlideUpWeapon()
    {
        StartCoroutine(SlideUpWeapon_Routine());
    }
    
    public void SlideDownWeaponLateUpdate()
    {
        if (isSliding)
        {
            rightShoulderBone.localEulerAngles = rightShoulderCurrentRotation;
            rightHandBone.localEulerAngles = rightHandStartRotation;
            rightElbowBone.localEulerAngles = rightElbowStartRotation; 
            weaponRoot.localEulerAngles = weaponRootRotation;
        }
    }

    private IEnumerator SlideDownWeapon_Routine()
    {
        isSliding = true;
        
        rightShoulderStartRotation = rightShoulderBone.localEulerAngles;
        slideTimer = 0;
        while (slideTimer < 1)
        {
            weaponRootRotation = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(70, 40, 60), slideTimer / 1);
            bipedIK.solvers.rightHand.IKPositionWeight = Mathf.Lerp(1, 0, slideTimer/1);
            rightShoulderCurrentRotation =
                Vector3.Lerp(rightShoulderStartRotation, rightShoulderStartRotation + handSlideVector, slideTimer / 1);
            slideTimer += Time.deltaTime;
            yield return null;
        }
        bipedIK.solvers.rightHand.IKPositionWeight = 0;
    }
    
    public IEnumerator SlideDownWeapon_RoutineTrial()
    {
        isSliding = true;

        rightHandStartRotation = rightHandBone.localEulerAngles;
        rightElbowStartRotation = rightElbowBone.localEulerAngles;
        rightShoulderStartRotation = rightShoulderBone.localEulerAngles;
        
        bipedIK.solvers.rightHand.IKPositionWeight = 0;
        slideTimer = 0;
        while (slideTimer < 1)
        {
            weaponRootRotation = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(70, 40, 60), SlideCurve.Evaluate(slideTimer / 1) );
            rightShoulderCurrentRotation =
                Vector3.Lerp(rightShoulderStartRotation, rightShoulderStartRotation + handSlideVector,SlideCurve.Evaluate(slideTimer / 1) );
            
            slideTimer += Time.deltaTime;
            yield return null;
        }

        rightShoulderCurrentRotation = rightShoulderStartRotation + handSlideVector;

    }
    
    public IEnumerator SlideUpWeapon_Routine()
    {
    
        slideTimer = 0;
        while (slideTimer < 1)
        {
            weaponRootRotation = Vector3.Lerp( new Vector3(70, 40, 60),new Vector3(0, 0, 0), SlideCurve.Evaluate(slideTimer / 1) );
            //bipedIK.solvers.rightHand.IKPositionWeight = Mathf.Lerp(1, 0, slideTimer/1);
            rightShoulderCurrentRotation =
                Vector3.Lerp(rightShoulderStartRotation + handSlideVector, rightShoulderStartRotation,SlideCurve.Evaluate(slideTimer / 1) );
            slideTimer += Time.deltaTime;
            yield return null;
        }
        bipedIK.solvers.rightHand.IKPositionWeight = 1;
        rightShoulderCurrentRotation = rightShoulderStartRotation;
        weaponRootRotation = new Vector3(0,0,0);
        isSliding = false;
    }
    
    public IEnumerator SlideDownWeapon_RoutineLast()
    {
        bipedIK.enabled = false;
        fullBodyBipedIK.enabled = false;

        float time = 0;
        float maxTime = 1;
        while (time < maxTime)
        {
            animator.SetLayerWeight(6,time/ maxTime);
            aimIK.solver.IKPositionWeight = Mathf.Lerp(1,0, time/ maxTime);
            aimIK.solver.poleWeight = Mathf.Lerp(1,0, time/ maxTime);
            time += Time.deltaTime;
            yield return null;
        }
    }
    
    public IEnumerator SlideUpWeapon_RoutineLast()
    {
        float time = 0;
        float maxTime = 1;
        while (time < maxTime)
        {
            animator.SetLayerWeight(6,Mathf.Lerp(1,0, time/ maxTime));
            aimIK.solver.IKPositionWeight = time/ maxTime;
            aimIK.solver.poleWeight = time/ maxTime;
            time += Time.deltaTime;
            yield return null;
        }
     
        bipedIK.enabled = true;
        fullBodyBipedIK.enabled = true;
    }

}
