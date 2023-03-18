using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<Weapon> allWeapons;
    
    public GameObject RightHandTarget;

    private void Awake()
    {
        InitWeapons();
    }

    public void InitWeapons()
    {
        allWeapons.Clear();
        foreach (Transform childWeaponGameObject in transform)
        {
            allWeapons.Add(childWeaponGameObject.GetComponent<Weapon>());
        }
    }

    public void ActivateWeaponWithId(string weaponId)
    {
        foreach (Weapon weapon in allWeapons)
        {
            bool status = weaponId == weapon.weaponId;
            weapon.gameObject.SetActive(status);
        }
    }
    
    public void ActivateWeaponWithWeaponReference(Weapon weapon)
    {
        foreach (Weapon currentWeapon in allWeapons)
        {
            bool status = weapon.weaponId == currentWeapon.weaponId;
            weapon.gameObject.SetActive(status);
        }
    }
}
