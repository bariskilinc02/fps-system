using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PivotPoint
{
    public static Vector3 FindCenterPoint(this GameObject original)
    {
        Vector3 offset = new Vector3(0, 0, 0);//original.transform.position; //- position;
        
        foreach (Transform child in original.transform)
            offset += child.transform.localPosition;
        
        offset /= original.transform.childCount + 1;
        return offset;
    }

    public static void SetPositionToCenterPoint(this GameObject original)
    {
        original.transform.position = -FindCenterPoint(original);
    }
    
    public static Vector3 CalculateLocalBounds(this GameObject original)
    {
        Quaternion currentRotation = original.transform.rotation;
        original.transform.rotation = Quaternion.Euler(0f,0f,0f);
        Bounds bounds = new Bounds(original.transform.position, Vector3.zero);
        
        foreach(Renderer renderer in original.GetComponentsInChildren<Renderer>())
        {
            bounds.Encapsulate(renderer.bounds);
        }
        Vector3 localCenter = bounds.center - original.transform.position;
        bounds.center = localCenter;
        bounds.extents *= 2;
        Debug.Log("The local bounds of this model is " + bounds);
        original.transform.rotation = currentRotation;

        return bounds.extents;
    }
}
