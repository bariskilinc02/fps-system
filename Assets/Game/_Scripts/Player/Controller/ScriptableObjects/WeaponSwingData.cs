using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSwingData", menuName = "ScriptableObject")]
public class WeaponSwingData : ScriptableObject
{
    public Curve2D DefaultHipSwing;
    public Curve2D DefaultAimSwing;
    
    public Curve2D HoldBreatheAimSwing;

    public Curve2D WalkHipSwing;
    public Curve2D WalkAimSwing;

    public Curve2D IdleBrokenArmHipSwing;
    public Curve2D IdleBrokenArmAimSwing;
    
    public Curve2D WalkBrokenArmHipSwing;
    public Curve2D WalkBrokenArmAimSwing;
   //public AnimationCurve verticalSwingCurve;
   //public AnimationCurve horizontalSwingCurve;
    
    
}

[System.Serializable]
public class Curve2D
{
    public AnimationCurve VerticalCurve;
    public float VerticalDistance;
    public AnimationCurve HorizontalCurve;
    public float HorizontalDistance;
    public float SwingSpeed;
}
