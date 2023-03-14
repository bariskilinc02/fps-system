using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] public Camera characterCamera;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject characterHead;
    [SerializeField] private GameObject aimIkPoleTarget;


    public float xSensivity;
    
    [SerializeField] private float verticalInput;
    [SerializeField] public float horizontalInput;

    [SerializeField] public float verticalAxis;
    [SerializeField] public float horizontalAxis;
    
    public RecoilHandler recoilHandler;

    public float test;

    public Transform SightLook;
    public float lerpValue;


    void Update()
    {
        if (PlayerManager.Instance.playerProperties.OnUI) return;
        CameraControl();
        //Placements();
    }

    private void LateUpdate()
    {
        Placements();
    }

    private void CameraControl()
    {
        verticalInput += Input.GetAxis("Mouse X")  * xSensivity;
        horizontalInput += -Input.GetAxis("Mouse Y")  * xSensivity;

        verticalAxis = Input.GetAxis("Mouse X");
        horizontalAxis = Input.GetAxis("Mouse Y");

        horizontalInput = Mathf.Clamp(horizontalInput, -90,90);

        character.transform.eulerAngles = new Vector3(character.transform.eulerAngles.x,verticalInput,character.transform.eulerAngles.z); 
        characterCamera.transform.eulerAngles = new Vector3(horizontalInput - recoilHandler.currentCameraRecoil.y,characterCamera.transform.eulerAngles.y,characterCamera.transform.eulerAngles.z);
    }

    private void Placements()
    {
        //0.015 - 0.065


        CameraPlacement();
        //float zPosition = horizontalInput > 0 ? 50 : -50;
        float angle = (horizontalInput + 90.0f) / 180.0f;
        float zPolePosition = Mathf.Lerp(-50, 50, angle);
        aimIkPoleTarget.transform.localPosition = new Vector3(0,20,zPolePosition);
        //4 21.5
        //float angle = (horizontalInput + 90.0f) / 180.0f;
        //
        //characterCamera.transform.localEulerAngles = characterCamera.transform.eulerAngles.With(y:yCameraAngle);

        // characterCamera.transform.localPosition =
        //     characterCamera.transform.localPosition.With(z: characterCamera.transform.localPosition.z + 0.5f);
        // float lerpValue = (horizontalInput + 90.0f) / 180.0f;
        // lerpValue = Mathf.Clamp(lerpValue, 0, 1);
        // Vector3 currentPosition = characterCamera.transform.localPosition;
        // characterCamera.transform.localPosition = currentPosition.With(x: Mathf.Lerp(0.015f, 0.061f, lerpValue));
    }

    private void CameraPlacement()
    {
        characterCamera.transform.position =
            Vector3.Lerp(characterHead.transform.position, SightLook.position, lerpValue);
        //characterCamera.transform.position = characterHead.transform.position +  characterHead.transform.forward * test ;
    }
}
