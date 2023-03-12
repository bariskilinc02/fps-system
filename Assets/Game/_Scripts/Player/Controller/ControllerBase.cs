using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Game._Scripts.Player.Controller
{
    public enum MovementState
    {
        Standing,
        Walking,
        Running,
    }
    public abstract class ControllerBase : MonoBehaviour
    {

        [SerializeField] private WeaponHandler weaponHandler;
        private const string movementStateId = "MovementState";
        private const string inputX = "InputX";
        private const string inputY = "InputY";
        private float currentMovementState;
        
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody rigidbody;

        public MovementState movementState;
        
        protected float verticalInput;//x - forward
        protected float horizontalInput;//y - right
        
        [SerializeField]protected float currentVerticalInput;//x - forward
        [SerializeField]protected float currentHorizontalInput;//y - right

        [Header("Speed Multipliers")]
        [SerializeField] protected float walkSpeed;
        [SerializeField] protected float runSpeed;
        [SerializeField] protected float crouchSpeed;
        [SerializeField] protected float currentSpeedMultiplier;
        
        [SerializeField] protected float movementSwitchSpeed;
        
        [SerializeField] protected bool isRunning;
        [SerializeField] protected bool isCrouching;
        [SerializeField] protected bool isHoldingBreath;

        [SerializeField] private PlayerCamera playerCamera;
        [SerializeField] private Transform spineTransform;
        [SerializeField] private Transform rightHandTransform;
        [SerializeField] private Transform characterRootObject;
        [SerializeField] private Transform root;

        [Header("Weapon Swing")] 
        [SerializeField] private WeaponSwingData weaponSwingData;
        private Curve2D currentCurveData;
        
        [SerializeField] private Transform weaponTargetTransform;
        private float weaponSwingTimer;

        private float weaponSmoothTurnHorizontalValue;
        private float weaponSmoothTurnVerticalValue;
        private float rootAnimatorValue;
        [Header("Recoil")] 
        [SerializeField] private RecoilHandler recoilHandler;
        private void Awake()
        {
            //Cursor.lockState = CursorLockMode.Locked;
        }

        protected void Update()
        {
            MovementInput();
            
            SwitchMovementState();
            ControlMovementSpeedState();
            ControlMovementState();

            SwitchCrouchState();
            ControlCrouchState();
            
            Animate();

            ControlSwingState();
            WeaponSwingAnimation();

            SwitchHoldingBreathState();
            ControlHoldingBreatheState();
            
        }
        
        protected void LateUpdate()
        {
            Move();
            Spine();
        }

        #region Movement
        
        private void Move()
        {
            Transform rigidbodyTransform = rigidbody.transform;
            
            Vector3 forward = rigidbodyTransform.forward;
            Vector3 right = rigidbodyTransform.right;
            
            Vector3 velocity = new Vector3();
            velocity = velocity.With(y: rigidbody.velocity.y);

            currentVerticalInput = Mathf.Lerp(currentVerticalInput, verticalInput, 10.0f * Time.deltaTime);
            currentHorizontalInput = Mathf.Lerp(currentHorizontalInput, horizontalInput, 10.0f * Time.deltaTime);

            currentVerticalInput = currentVerticalInput.Abs() < 0.01f ? 0 : currentVerticalInput;
            currentHorizontalInput = currentHorizontalInput.Abs() < 0.01f ? 0 : currentHorizontalInput;
            
            Vector3 finalInput = new Vector3(currentVerticalInput, currentHorizontalInput,0);
            
            if ((currentHorizontalInput.Abs() + currentVerticalInput.Abs()) is < 1.5f and > -1.5f)
            {
         
            }
            else
            {
                finalInput.Normalize();
            }
         
            float forwardBackwardValue = isCrouching ? currentSpeedMultiplier : finalInput.x < 0 ? walkSpeed : currentSpeedMultiplier;
            float leftRightValue = isCrouching ? currentSpeedMultiplier : finalInput.x > 0.3f ? currentSpeedMultiplier : walkSpeed;
            
            velocity = forward * finalInput.x * forwardBackwardValue + right * finalInput.y * leftRightValue;
            velocity = velocity.With(y: rigidbody.velocity.y);
            
            rigidbody.velocity = velocity;
        }

        protected abstract void MovementInput();
        
        private void ControlMovementState()
        {
            if (currentHorizontalInput.Abs() + currentVerticalInput.Abs() > 0)
            {
                if (rigidbody.velocity.magnitude > 2.1f)
                {
                    movementState = MovementState.Running;
                }
                else
                {
                    movementState = MovementState.Walking;
                }
            }
            else
            {
                movementState = MovementState.Standing;
            }
        }
        
        private void SwitchMovementState()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isRunning = !isRunning;
            }
        }
        
        private void ControlMovementSpeedState()
        {
            float currentMultiplier = isCrouching ? crouchSpeed : (isRunning ? runSpeed : walkSpeed) ;
            currentSpeedMultiplier = Mathf.Lerp(currentSpeedMultiplier, currentMultiplier, 10 * Time.deltaTime);
        }

        #endregion
        
        #region Crouch
        private void SwitchCrouchState()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
            }
        }
        private void ControlCrouchState()
        {
            rootAnimatorValue = Mathf.Lerp(rootAnimatorValue,  isCrouching ? 1: 0, 5 *Time.deltaTime);
            animator.SetFloat("root_crouch", rootAnimatorValue);
            //characterRootObject.localPosition = Vector3.Lerp(characterRootObject.localPosition, characterRootObject.localPosition.With(y: isCrouching ? -0.4f : 0f), movementSwitchSpeed * Time.deltaTime); ;
        }

        #endregion
        
        #region Weapon Swing
        private void ControlSwingState()
        {
            if (weaponHandler.onAim == false)
            {
                if (movementState == MovementState.Standing)
                {
                    currentCurveData = weaponSwingData.DefaultHipSwing;
                }
                else if(movementState == MovementState.Walking)
                {
                    currentCurveData = weaponSwingData.WalkHipSwing;
                }
                else if (movementState == MovementState.Running)
                {
                    currentCurveData = weaponSwingData.DefaultHipSwing;
                } 
            }
            else
            {
                if (movementState == MovementState.Standing)
                {
                    if (isHoldingBreath)
                    {
                        currentCurveData = weaponSwingData.HoldBreatheAimSwing;
                    }
                    else
                    {
                        currentCurveData = weaponSwingData.DefaultAimSwing;
                    }
                 
                }
                else if(movementState == MovementState.Walking)
                {
                    currentCurveData = weaponSwingData.WalkAimSwing;
                }
                else if (movementState == MovementState.Running)
                {
                    currentCurveData = weaponSwingData.DefaultHipSwing;
                } 
            }
        
        }
        private void WeaponSwingAnimation()
        {
            weaponSwingTimer += currentCurveData.SwingSpeed * Time.deltaTime;

            if (weaponSwingTimer > 1)
            {
                weaponSwingTimer = 0;
            }

            weaponSmoothTurnVerticalValue += playerCamera.horizontalAxis;
            weaponSmoothTurnVerticalValue = Mathf.Lerp(weaponSmoothTurnVerticalValue, 0, 15 *Time.deltaTime);
            
            weaponSmoothTurnHorizontalValue += playerCamera.verticalAxis;
            weaponSmoothTurnHorizontalValue = Mathf.Lerp(weaponSmoothTurnHorizontalValue, 0, 15 *Time.deltaTime);
            
            float horizontalSwing = currentCurveData.HorizontalCurve.Evaluate(weaponSwingTimer) * currentCurveData.HorizontalDistance + recoilHandler.currentWeaponRecoil.x - weaponSmoothTurnHorizontalValue ;
            float verticalSwing = currentCurveData.VerticalCurve.Evaluate(weaponSwingTimer) * currentCurveData.VerticalDistance + recoilHandler.currentWeaponRecoil.y - weaponSmoothTurnVerticalValue;
            Vector3 targetPosition = new Vector3(horizontalSwing,verticalSwing,120);
            weaponTargetTransform.localPosition = Vector3.Lerp(  weaponTargetTransform.localPosition,targetPosition,10 * Time.deltaTime);
        }
        #endregion

        #region Holding Breathe

        private void SwitchHoldingBreathState()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (movementState == MovementState.Standing && weaponHandler.onAim)
                {
                    isHoldingBreath = !isHoldingBreath;
                }
            }
        }

        private void ControlHoldingBreatheState()
        {
            if (movementState != MovementState.Standing || weaponHandler.onAim == false)
            {
                isHoldingBreath = false;
            }
            
            
        }

        #endregion
        private void Spine()
        {

           float lerpValue = (playerCamera.horizontalInput + 90.0f) / 180.0f;
           lerpValue = Mathf.Clamp(lerpValue, 0, 1);
           //root.rotation = new Quaternion(0.997269094f, -0.0106667271f, 0.0354857855f, -0.0638849288f);
           
           //Vector3 currentHandRotation = rightHandTransform.localRotation.eulerAngles;
           //Debug.Log(lerpValue);
           //rightHandTransform.localEulerAngles = rightHandTransform.localEulerAngles.With(x: Mathf.Lerp(160.8f, 173.9f, lerpValue));

           // spineTransform.localRotation = Quaternion.Euler(spineTransform.localEulerAngles.With(x:0));
           // spineTransform.localRotation = Quaternion.Euler( spineTransform.localEulerAngles.With(z:playerCamera.horizontalInput - 180));


           //spineTransform.rotation = Quaternion.Euler(root.up);
           //spineTransform.localRotation = quaternion.Euler(spineTransform.localRotation.eulerAngles + new Vector3(0,0,xxxx));
           //spineTransform.LookAt(target.transform);
           //spineTransform.eulerAngles = spineTransform.eulerAngles.With(y:spineTransform.eulerAngles.y);

           //spineTransform.localRotation = Quaternion.Euler(spineTransform.localEulerAngles.With(y:180));

           //spineTransform.localRotation = Quaternion.Euler(rigidbody.transform.forward);
           //spineTransform.localRotation = Quaternion.Euler(spineTransform.localEulerAngles.With(z:playerCamera.horizontalInput - 180 * rigidbody.transform.forward.x));
        }

        private void Animate()
        {
            float speed = rigidbody.velocity.magnitude;
            
            if (isCrouching)
            {
                currentMovementState = Mathf.Lerp(currentMovementState, -1, movementSwitchSpeed * Time.deltaTime);
            }
            else if (verticalInput == 0 && horizontalInput == 0)
            {
                currentMovementState = Mathf.Lerp(currentMovementState, 0, movementSwitchSpeed * Time.deltaTime);
            }
            else if (speed < 2)
            {
                currentMovementState = Mathf.Lerp(currentMovementState, 1, movementSwitchSpeed * Time.deltaTime);
            }
            else
            {
                currentMovementState = Mathf.Lerp(currentMovementState, 2, movementSwitchSpeed * Time.deltaTime);
            }
            
            animator.SetFloat(movementStateId, currentMovementState);
            animator.SetFloat(inputX, currentVerticalInput);
            animator.SetFloat(inputY, currentHorizontalInput);
        }
        
    }
}
