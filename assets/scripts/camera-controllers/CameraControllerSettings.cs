using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia
{
    [System.Serializable]
    public class CameraControllerSettings
    {
        /// <summary>
        /// Whether or not this camera controller is enabled. If it is, then
        /// do the functionality.. this option is good for systems where the camera controller
        /// is separate from the character movement.
        /// </summary>
        public bool enabled = true;

        // public enum CameraControllerType
        // {
        //     BLANK, FIXED
        // }

        // public Dictionary<CameraControllerType, System.Type> cameraControllerMap = new Dictionary<CameraControllerType, System.Type>()
        // {
        //     { CameraControllerType.BLANK, typeof(BlankCameraController) }
        // }
    }
}