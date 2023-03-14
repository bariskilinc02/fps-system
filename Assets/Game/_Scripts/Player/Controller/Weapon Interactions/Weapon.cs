using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public string weaponId;
    [HideInInspector] public GameObject weaponGameObject;
    [HideInInspector] public GameObject leftHandIKTarget;
    public Vector3 rightHandDefaultPosition;
    public Vector3 rightHandAimPosition;

    [HideInInspector] public BarrelHandler barrelHandler;
    [HideInInspector] public GripHandler gripHandler;
    [HideInInspector] public SightHandler sightHandler;
    [HideInInspector] public MuzzleHandler muzzleHandler;

    public BarrelPart currentBarrel;
    public GripPart currentGrip;
    public SightPart currentSight;
    public MuzzlePart currentMuzzle;
    
    public AnimatorOverrideController weaponOverrider;
    
    public void BuildWeapon()
    {
        weaponGameObject = gameObject;
        leftHandIKTarget = weaponGameObject.FindChildWithName("LeftHandTarget");

        barrelHandler =  GetComponentInChildren<BarrelHandler>();
        barrelHandler.GetAllBarrelParts();
        currentBarrel = barrelHandler.GetActiveBarrelPart();

        gripHandler = currentBarrel.ReturnActiveGripHandler();
        gripHandler.GetAllGripParts();
        currentGrip = gripHandler.GetCurrentGripPart();
        leftHandIKTarget = currentGrip.leftHandTarget.gameObject;
        //gripHandler = GetComponentInChildren<GripHandler>();
        //gripHandler.GetAllGripParts();
        //currentGrip = gripHandler.GetCurrentGripPart();
        //leftHandIKTarget = currentGrip.leftHandTarget.gameObject;
        
        sightHandler = GetComponentInChildren<SightHandler>();
        sightHandler.GetAllSightParts();
        sightHandler.SetSightPart();
        currentSight = sightHandler.GetCurrentSigthPart();
        
        muzzleHandler = currentBarrel.ReturnActiveMuzzleHandler();
        muzzleHandler.GetAllMuzzleParts();
        currentMuzzle = muzzleHandler.GetCurrentMuzzlePart();
        //muzzleHandler = GetComponentInChildren<MuzzleHandler>();
        //muzzleHandler.GetAllMuzzleParts();
        //currentMuzzle = muzzleHandler.GetCurrentMuzzlePart();
        
        rightHandAimPosition = rightHandAimPosition.With(y: sightHandler.GetAimPositionX());
    }
    
    
}
