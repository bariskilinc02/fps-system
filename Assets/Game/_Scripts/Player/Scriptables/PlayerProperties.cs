using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Properties", menuName = "Scriptables/Player Properties")]
public class PlayerProperties : ScriptableObject
{
    public bool CursorLockState;
    public bool OnUI;
}
