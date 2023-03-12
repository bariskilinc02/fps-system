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

    [HideInInspector] public GripHandler gripHandler;
    [HideInInspector] public SightHandler sightHandler;

    public GripPart currentGrip;
    private void OnEnable()
    {
   
    }
    
    public void BuildWeapon()
    {
        weaponGameObject = gameObject;
        leftHandIKTarget = weaponGameObject.FindChildWithName("LeftHandTarget");
        
        gripHandler = GetComponentInChildren<GripHandler>();
        gripHandler.GetAllGripParts();
        currentGrip = gripHandler.GetCurrentGripPart();
        leftHandIKTarget = currentGrip.leftHandTarget.gameObject;
        
        sightHandler = GetComponentInChildren<SightHandler>();
        sightHandler.GetAllSightParts();
        sightHandler.SetSightPart();
        
        rightHandAimPosition = rightHandAimPosition.With(y: sightHandler.GetAimPositionX());
    }
    
    
}
