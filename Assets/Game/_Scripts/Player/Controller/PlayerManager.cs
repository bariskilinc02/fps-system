using System;
using System.Collections;
using System.Collections.Generic;
using Game._Scripts.Player.Controller;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerController playerController;
    public PlayerProperties playerProperties;
    public PlayerCamera playerCamera;
    public Animator animator;
    public WeaponHandler weaponHandler;
    public Weapons weapons;
    public RecoilHandler recoilHandler;
    public ReloadHandler reloadHandler;
    public SlideHandler slideHandler;

    [Header("IK")]
    public FullBodyBipedIK fullBodyBipedIK;
    public BipedIK bipedIK;

    private void Start()
    {
        playerProperties.OnUI = false;
        playerProperties.CursorLockState = true;
    }

    private void Update()
    {
        CursorLockHandler();
    }

    public void CursorLockHandler()
    {
        Cursor.lockState = playerProperties.CursorLockState ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public void ChangeWeaponGripPart(int i)
    {
        weaponHandler.currentWeapon.gripHandler.CustomizeGripPart(i);
        weaponHandler.SetWeapon(weaponHandler.currentWeapon.weaponId);
    }
    
    public void ChangeWeaponSightPart(int i)
    {
        weaponHandler.currentWeapon.sightHandler.CustomizeSightPart(i);
        weaponHandler.SetWeapon(weaponHandler.currentWeapon.weaponId);
    }
}
