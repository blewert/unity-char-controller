using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia
{
    //A character controller at least requires:
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    
    //Add a menu item
    [AddComponentMenu("Bloktopia/Character Controller")]

    public class CharacterController : MonoBehaviour
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
        /// Called on start-up
        /// </summary>
        private void Start()
        { 
            //Instantiate camera controller (for now this is manual)
            cameraController = new CameraController.BlankCameraController();

            //Is the camera controller enabled? If so, start it..
            if(cameraSettings.enabled)
                cameraController?.Init(in cameraSettings);
        }



        /// <summary>
        /// Called once per frame
        /// </summary>
        void Update()
        {      
            //Enabled? Update the camera controller
            if(cameraSettings.enabled)
                cameraController?.Update();
        }
    }
}