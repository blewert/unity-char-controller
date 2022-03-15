using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

namespace Bloktopia
{
    //A character controller at least requires:
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    
    //Add a menu item
    [AddComponentMenu("Bloktopia/Character Controller")]

    public class BaseCharacterController : MonoBehaviour
    {    
        /// <summary>
        /// The camera controller for this character controller. Each go 
        /// hand-in-hand with another
        /// </summary>
        [Header("Cameras")]
        private ICameraController cameraController;

        /// <summary>
        /// The camera controller settings.
        /// </summary>
        public CameraControllerSettings cameraSettings; 

        /// <summary>
        /// Character controller settings
        /// </summary>
        public CharacterControllerSettings controllerSettings;

        /// <summary>
        /// The rigidbody attached to this object
        /// </summary>
        private new Rigidbody rigidbody;

        /// <summary>
        /// Is this object grounded?
        /// </summary>
        protected bool isGrounded = false;

        /// <summary>
        /// Tracked character movement 
        /// </summary>
        protected Vector2 characterMovement = default;

        public void OnJumpInput(InputAction.CallbackContext context)
        {
            //Not finished? Get out of here
            if(!context.performed)
                return;

            //Not grounded? Get out of here
            if(!isGrounded)
                return; 

            //Set the trigger for jumping 
            if(controllerSettings.playerAnimator != null)
                controllerSettings.playerAnimator.SetTrigger(controllerSettings.jumpAnimationTriggerVariable);

            //Add the force
            rigidbody.AddForce(Vector3.up * controllerSettings.jumpImpulseSpeed, ForceMode.Impulse);
        }

        public void OnLookInput(InputAction.CallbackContext context)
        {

        }

        public void OnAttackInput(InputAction.CallbackContext context)
        {
            //Set character movement if held
            if (context.performed && controllerSettings.playerAnimator != null)
                controllerSettings.playerAnimator.SetTrigger(controllerSettings.animationAttackTriggerVariable);
        }

        public void OnMoveInput(InputAction.CallbackContext context)
        {
            //Set character movement if held
            if(context.performed)
                characterMovement = context.ReadValue<Vector2>();

            //Otherwise, reset
            else if(context.canceled)
                characterMovement = default;
        }


        public void OnCollisionEnter(Collision col) => isGrounded = true;
        public void OnCollisionExit(Collision col) => isGrounded = false;
        
        protected void SetupRigidbody()
        {
            //Set rigidbody
            rigidbody = GetComponent<Rigidbody>();

            //Is it null?
            if (rigidbody == null)
                throw new UnityException("Rigidbody is null!");

            //Do we need to freeze the rotation?
            if(controllerSettings.freezeRollPitchRotation)
                rigidbody.constraints |= (RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ);
        }

        /// <summary>
        /// Called on start-up
        /// </summary>
        private void Start()
        { 
            //Set up the rigidbody
            this.SetupRigidbody();

            //Instantiate camera controller (for now this is manual)
            cameraController = new FixedLockCameraController();

            //Is the animator null?
            if(controllerSettings.playerAnimator == null)
                Debug.LogWarning("Warning: Player animator not assigned in inspector.");

            //Is the camera controller enabled? If so, start it..
            if(cameraSettings.enabled)
                cameraController?.Init(cameraSettings, transform, Camera.main);
        }

        protected bool IsInImmovableState()
        {
            if(controllerSettings.playerAnimator == null)
                return false;

            var clipInfo = controllerSettings.playerAnimator.GetCurrentAnimatorClipInfo(0);

            foreach(var info in clipInfo)
            {
                //This clip is an immovable clip name? return true
                if(controllerSettings.immovableMovementClips.Any(x => x.Equals(info.clip.name)))
                    return true;
            }
            
            //Otherwise return false
            return false;
        }

        protected void UpdateCharacterMovement()
        {
            //Not grounded? do nothing
            // if(!isGrounded)
                // return;

            //Are they in an immovable state? If so.. get out
            if(IsInImmovableState())
                return;

            //Set animation variables
            if (controllerSettings.playerAnimator != null)
                controllerSettings.playerAnimator.SetFloat(controllerSettings.animationMovementVariable, characterMovement.normalized.magnitude);

            //No movement? don't bother
            if(characterMovement.normalized.magnitude < 0.001f)
                return;

            
            //Get movement vector in camera space, cancel y
            var movementVec = Camera.main.transform.TransformDirection(new Vector3(characterMovement.x, 0, characterMovement.y)).normalized;

            //Project onto the xz plane and normalize because we dont care about ||v||
            var projectedVec = Vector3.ProjectOnPlane(movementVec, Vector3.up).normalized;

            //Slerp rotation from current to target lookat rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(projectedVec, Vector3.up), Time.deltaTime * controllerSettings.turnSpeed);

            //Add a force in this direction, scaled by input axes magnitude
            rigidbody.AddForce((transform.forward * controllerSettings.maxMovementForce * Time.deltaTime) * characterMovement.magnitude, ForceMode.Force);
        }

        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {      
            //Enabled? Update the camera controller
            if(cameraSettings.enabled)
                cameraController?.Update();

            //Update character movement
            UpdateCharacterMovement();
        }
    }
}