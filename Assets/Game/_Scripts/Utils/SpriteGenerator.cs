using System.Collections;
using System.Collections.Generic;
using ThumbCreator.Helpers;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
[ExecuteInEditMode]
public class SpriteGenerator : MonoBehaviour
{
    public Camera targetCamera;
    [Header("Target Settings")]
    public GameObject targetGameObject;
    [Range(0, 360)]
    public int RotationX;
    [Range(0, 360)]
    public int RotationY;
    [Range(0, 360)]
    public int RotationZ;
    [Header("Camera Settings")]
    public bool isCameraOrthographic;
    //public bool isCameraBackgroundTransparent;
    public Color CameraBackgroundColor;
    [Range(-20, 20)]
    public int CameraX;
    [Range(-20, 20)]
    public int CameraY;
    [Range(0, -100)]
    public float CameraZ = -8;
    [Header("Export Settings")]
    public string Filename = "Image";
    public FileType ExportFile = FileType.Png;
        
      
    [Range(16,4096)]
    public int m_width = 256;
            
    [Range(16, 4096)]
    public int m_height = 256;

    private Texture2D tempTexture;
    public Image spritePreview;

    void Update()
    {
        var objRot = transform.rotation.eulerAngles;
        var newRot = new Vector3(RotationX, RotationY, RotationZ);
        if (objRot != newRot)
            transform.localRotation = Quaternion.Euler(RotationX, RotationY, RotationZ);

        var camPos = targetCamera.transform;
        targetCamera.backgroundColor = CameraBackgroundColor;
        var newPos = new Vector3(CameraX, CameraY, CameraZ);
        if (camPos.position != newPos)
        {
            targetCamera.orthographic = isCameraOrthographic;
            if (isCameraOrthographic)
                targetCamera.orthographicSize = CameraZ * -1;
            camPos.localPosition = newPos;
        }
    }

    public void Take()
    {
        RotateItem();
        GenerateFile();
    }

    public void RotateItem()
    {
        transform.localRotation = Quaternion.Euler(RotationX, 0, RotationZ);
    }

    public void GenerateFile()
    {
        switch (ExportFile)
        {
            case FileType.Png:
                tempTexture = Screenshot.GeneratePng(Camera.main,Filename, m_width, m_height);
                break;
            case FileType.Sprite:
                StartCoroutine(TakeShot());
                break;
            default:
                break;
        }
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    private void GenerateSpriteNew(Texture2D tex)
    {
        Sprite sprite;//= new Sprite();
        spritePreview.sprite = Sprite.Create(tex,new Rect(0, 0, m_width, m_height),new Vector2(50,50), 200f);
    }
        
    private IEnumerator TakeShot()
    {
        //bound 1 => 64
        //bound 5 = 5 * 64
        Vector3 objectExtends = targetGameObject.CalculateLocalBounds();
        targetGameObject.SetPositionToCenterPoint();
        m_width = (int) (objectExtends.x * 64);
        
        spritePreview.GetComponent<RectTransform>().sizeDelta = new Vector2(100 * objectExtends.x , 100);
        
        tempTexture = Screenshot.GeneratePng(targetCamera,Filename, m_width, m_height);
        tempTexture.Apply();
            
        yield return new WaitForSeconds(0);
            
        GenerateSpriteNew(tempTexture);
    }
}

public enum FileType
{
    Png,
    Sprite
}
