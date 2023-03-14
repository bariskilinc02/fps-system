using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightComponentButton : ComponentButton
{
    
    public override void OnClickedButton()
    {
        PlayerManager.Instance.ChangeWeaponSightPart(index);
    }
}
