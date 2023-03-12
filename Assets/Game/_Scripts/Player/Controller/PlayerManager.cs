using System.Collections;
using System.Collections.Generic;
using Game._Scripts.Player.Controller;
using RootMotion.FinalIK;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerCamera playerCamera;
    public Animator animator;
    public WeaponHandler weaponHandler;
    public RecoilHandler recoilHandler;
    public ReloadHandler reloadHandler;
    public SlideHandler slideHandler;

    [Header("IK")]
    public FullBodyBipedIK fullBodyBipedIK;
    public BipedIK bipedIK;
}
