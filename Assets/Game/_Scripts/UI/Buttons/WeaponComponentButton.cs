using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponentButton : ComponentButton
{
    public override void OnClickedButton()
    {
        PlayerManager.Instance.weaponHandler.SetWeapon(PlayerManager.Instance.weapons.allWeapons[index].weaponId);
    }
}
