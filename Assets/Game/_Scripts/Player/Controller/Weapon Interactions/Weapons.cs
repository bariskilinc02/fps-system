using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public List<Weapon> allWeapons;

    public GameObject CrossCube;
    public GameObject RightHandTarget;

    private void Awake()
    {
        allWeapons.Clear();
        foreach (Transform childWeaponGameObject in transform)
        {
           
            allWeapons.Add(childWeaponGameObject.GetComponent<Weapon>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            CalculateHandAimPosition();
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

    private void CalculateHandAimPosition()
    {
        Vector3 newVector = RightHandTarget.transform.position - CrossCube.transform.position;
        Debug.Log(newVector.y);
    }
}
