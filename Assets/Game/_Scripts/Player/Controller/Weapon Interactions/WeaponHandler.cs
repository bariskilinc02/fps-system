using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
    
    [SerializeField] private Transform rightHandIKTarget;
    [SerializeField] private Weapons weapons;
    [SerializeField] private Animator animator;
    public string testId;

    public bool onAim;
    public bool onSlide;
    public float aimValue;
 

    public Weapon currentWeapon;

    public bool active;
    

    public RecoilHandler RecoilHandler;

    public float fireTime;

    public SlideHandler slideHandler;


    private void Start()
    {
        ChangeWeapon(testId);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            SetGrip(currentWeapon.weaponId);
        }
        //Debug.Log(weapons.transform.eulerAngles);
    
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon(testId);
           
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeapon("Revolver");
        }
        
        AimController();
      

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

    public void SetWeapon(string weaponId)
    {
        currentWeapon = weapons.allWeapons.Find(x => x.weaponId == weaponId);
        currentWeapon.BuildWeapon();
        animator.runtimeAnimatorController = currentWeapon.currentGrip.gridOverrider;
        weapons.ActivateWeaponWithId(weaponId);
        SetLeftHandIKTarget(weaponId);


    }

    private void Aim()
    {
        if(currentWeapon == null) return;
        
     
        if (onAim)
        {
            //aimValue += 1 * Time.deltaTime;
            aimValue = Mathf.Lerp(aimValue, 1, 10 * Time.deltaTime);
        }
        else
        {
            //aimValue -= 1 * Time.deltaTime;
            aimValue = Mathf.Lerp(aimValue, 0, 10 * Time.deltaTime);
        }

        aimValue = Mathf.Clamp(aimValue, 0, 1);

        rightHandIKTarget.localPosition = Vector3.Lerp(currentWeapon.rightHandDefaultPosition,
            currentWeapon.rightHandAimPosition, aimValue);
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

    public void SetGrip(string weapon)
    {
        SetWeapon(weapon);
        currentWeapon.BuildWeapon();
    }
    
}
