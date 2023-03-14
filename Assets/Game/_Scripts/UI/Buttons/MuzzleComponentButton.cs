using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleComponentButton : ComponentButton
{
    public override void OnClickedButton()
    {
        PlayerManager.Instance.ChangeWeaponMuzzlePart(index);
    }
}
