using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripComponentButton : ComponentButton
{
    public GripPart targetGripPart;
    
    public override void OnClickedButton()
    {
        PlayerManager.Instance.ChangeWeaponGripPart(index);
    }
}
