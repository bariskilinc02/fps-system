using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCustomizationUI : MonoBehaviour
{
    public GameObject weaponCustomizationPage;
    public GameObject gridListPage;
    public GameObject sightListPage;
    public Button openPanelButton;
    public Button closePanelButton;
    
    public Weapons weapons;
    public WeaponHandler weaponHandler;

    public Transform gripContent;
    public Transform sightContent;
    public GameObject gripButtonPrefab;
    public GameObject sightButtonPrefab;
    
    public Weapon currentWeapon;

    public Button gripListButton;
    public Button sightListButton;

    private void Awake()
    {
        gripListButton.onClick.AddListener(OpenGripList);
        sightListButton.onClick.AddListener(OpenSightList);
        
        openPanelButton.onClick.AddListener(OpenCustomizationPage);
        closePanelButton.onClick.AddListener(CloseCustomizationPage);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OpenCustomizationPage();
       
        }
    }

    private void ListWeaponGripComponents()
    {
        for (int i = gripContent.childCount - 1; i >= 0 ; i--)
        {
            Destroy(gripContent.GetChild(i).gameObject);
        }
        
        currentWeapon = weaponHandler.currentWeapon;

        List<GripPart> gripParts = currentWeapon.gripHandler.gripParts;
        for (int i = 0; i < gripParts.Count; i++)
        {
            ComponentButton componentButton = Instantiate(gripButtonPrefab, gripContent).GetComponent<ComponentButton>();
            componentButton.index = i;
        }
    }
    
    private void ListWeaponSightComponents()
    {
        for (int i = sightContent.childCount - 1; i >= 0 ; i--)
        {
            Destroy(sightContent.GetChild(i).gameObject);
        }
        
        currentWeapon = weaponHandler.currentWeapon;

        List<SightPart> sightParts = currentWeapon.sightHandler.sigthParts;
        for (int i = 0; i < sightParts.Count; i++)
        {
            ComponentButton componentButton = Instantiate(sightButtonPrefab, sightContent).GetComponent<ComponentButton>();
            componentButton.index = i;
        }
    }
    
    public void OpenGripList()
    {
        gridListPage.SetActive(true);
        sightListPage.SetActive(false);
    }
    public void OpenSightList()
    {
        gridListPage.SetActive(false);
        sightListPage.SetActive(true);
    }

    public void OpenCustomizationPage()
    {
        weaponCustomizationPage.SetActive(true);
        ListWeaponGripComponents();
        ListWeaponSightComponents();
        PlayerManager.Instance.playerProperties.OnUI = true;
        PlayerManager.Instance.playerProperties.CursorLockState = false;
    }
    
    public void CloseCustomizationPage()
    {
        weaponCustomizationPage.SetActive(false);
        PlayerManager.Instance.playerProperties.OnUI = false;
        PlayerManager.Instance.playerProperties.CursorLockState = true;
    }
}
