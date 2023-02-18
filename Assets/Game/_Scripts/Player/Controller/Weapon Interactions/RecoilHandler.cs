using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RecoilHandler : MonoBehaviour
{
    public float verticalRecoil;
    public float horizontalRecoil;
    public float fixTime;
    
    public float camFixTime;
    public float verticalCameraRecoil;
    public float horizontalCameraRecoil;

    public Vector2 currentWeaponRecoil;
    public Vector2 currentCameraRecoil;

    public void Update()
    {
        RecoilControl();
    }

    public void RecoilControl()
    {
        currentWeaponRecoil = Vector2.Lerp(currentWeaponRecoil, new Vector2(0,0), fixTime * Time.deltaTime);
        
        currentCameraRecoil = Vector2.Lerp(currentCameraRecoil, new Vector2(0,0), fixTime * Time.deltaTime);
    }

    public void Recoil()
    {
        currentWeaponRecoil += new Vector2(Random.Range(-horizontalRecoil, horizontalRecoil), verticalRecoil);
        StartCoroutine(CameraRecoilRoutine());
        //currentCameraRecoil += new Vector2(horizontalCameraRecoil,  verticalCameraRecoil );
    }

    private IEnumerator CameraRecoilRoutine()
    {
        float time = 0;
        while (time < camFixTime)
        {
            currentCameraRecoil += new Vector2(horizontalCameraRecoil,  verticalCameraRecoil );
            time += Time.deltaTime;
            yield return null;
        }
    }
}
