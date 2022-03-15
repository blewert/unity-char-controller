using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


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
        private CameraController.ICameraController cameraController;

        /// <summary>
        /// The camera controller settings.
        /// </summary>
        public CameraController.Settings cameraSettings; 

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

            rigidbody.AddForce(Vector3.up * controllerSettings.jumpImpulseSpeed, ForceMode.Impulse);
        }

        public void OnLookInput(InputAction.CallbackContext context)
        {

        }

        public void OnAttackInput(InputAction.CallbackContext context)
        {

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
            cameraController = new CameraController.BlankCameraController();


            //Is the camera controller enabled? If so, start it..
            if(cameraSettings.enabled)
                cameraController?.Init(in cameraSettings);
        }

        protected void UpdateCharacterMovement()
        {
            //Not grounded? do nothing
            // if(!isGrounded)
                // return;
                
            // Debug.Log(characterMovement);

            //Cool, so now we need to move the character. Apply a force forward:
            rigidbody.AddForce((transform.forward * controllerSettings.maxMovementForce) * characterMovement.y, ForceMode.Force);

            //And turn
            rigidbody.AddTorque(transform.up * characterMovement.x * controllerSettings.turnSpeed, ForceMode.Force);
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