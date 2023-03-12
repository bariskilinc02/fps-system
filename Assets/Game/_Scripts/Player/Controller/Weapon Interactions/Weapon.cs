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

    [HideInInspector] public SightHandler sightHandler;

    private void OnEnable()
    {
   
    }

    public void BuildWeapon()
    {
        weaponGameObject = gameObject;
        leftHandIKTarget = weaponGameObject.FindChildWithName("LeftHandTarget");
        
        sightHandler = GetComponentInChildren<SightHandler>();
        sightHandler.SetSightPart();
        rightHandAimPosition = rightHandAimPosition.With(y: sightHandler.GetAimPositionX());
    }
    
    
}
