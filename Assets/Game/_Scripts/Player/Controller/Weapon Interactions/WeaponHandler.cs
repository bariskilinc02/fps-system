using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    private FullBodyBipedIK fullBodyBipedIK;
    private Weapons weapons;
    private Animator animator;
    private SlideHandler slideHandler;
    private RecoilHandler RecoilHandler;
    private Camera playerCamera;
    
    [SerializeField] private Transform rightHandIKTarget;
    
    public string testId;

    public bool onAim;
    public bool onSlide;
    public float adsTime;
    
    public Weapon currentWeapon;
    
    public float fireTime;



    private void Awake()
    {
        fullBodyBipedIK = playerManager.fullBodyBipedIK;
        weapons = playerManager.weapons;
        animator = playerManager.animator;
        RecoilHandler = playerManager.recoilHandler;
        slideHandler = playerManager.slideHandler;
        playerCamera = playerManager.playerCamera.characterCamera;
    }

    private void Start()
    {
        ChangeWeapon(testId);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon(testId);
           
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeapon("ak47");
        }
        
        AimController();

        if (PlayerManager.Instance.playerProperties.OnUI) return;
        
        Aim();
        if(onSlide) return;
        Fire();

    }

    private void LateUpdate()
    {
       // slideHandler.SlideDownWeaponLateUpdate();
    }

    private void SetLeftHandIKTarget(string weaponId)
    {
        fullBodyBipedIK.solver.leftHandEffector.target = weapons.allWeapons.Find(x => x.weaponId == weaponId).currentGrip.leftHandTarget;
    }
    
    private void Aim()
    {
        if(currentWeapon == null) return;
        
     
        if (onAim)
        {
            //aimValue += 1 * Time.deltaTime;
            adsTime = Mathf.Lerp(adsTime, 1, 10 * Time.deltaTime);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 60, 10 * Time.deltaTime);
        }
        else
        {
            //aimValue -= 1 * Time.deltaTime;
            adsTime = Mathf.Lerp(adsTime, 0, 10 * Time.deltaTime);
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, 80, 10 * Time.deltaTime);
        }

        adsTime = Mathf.Clamp(adsTime, 0, 1);

        rightHandIKTarget.localPosition = Vector3.Lerp(currentWeapon.rightHandDefaultPosition,
            currentWeapon.rightHandAimPosition, adsTime);
    }

    private void AimController()
    {
        if (onSlide) return;
        
        if (Input.GetMouseButtonDown(1))
        {
            onAim = !onAim;
        }
    }
    
    private void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RecoilHandler.Recoil();
        }
        
        if (fireTime < 0)
        {
            if (Input.GetMouseButton(0))
            {
                fireTime = 0.1f;
                RecoilHandler.Recoil();
            }
        }

        fireTime -= Time.deltaTime;

    }

    private void ChangeWeapon(string id)
    {
        if (onSlide) return;
        onAim = false;
        
        StartCoroutine(ChangeWeapon_Routine(id));
    }

    private IEnumerator ChangeWeapon_Routine(string id)
    {
        onSlide = true;
        yield return slideHandler.SlideDownWeapon_RoutineLast();
        SetWeapon(id);
        yield return slideHandler.SlideUpWeapon_RoutineLast();
        onSlide = false;
    }
    
    public void SetWeapon(string weaponId)
    {
        currentWeapon = weapons.allWeapons.Find(x => x.weaponId == weaponId);
        currentWeapon.BuildWeapon();
        animator.runtimeAnimatorController = currentWeapon.weaponOverrider;
        animator.runtimeAnimatorController = currentWeapon.currentGrip.gridOverrider;
        weapons.ActivateWeaponWithId(weaponId);
        SetLeftHandIKTarget(weaponId);
    }

}
