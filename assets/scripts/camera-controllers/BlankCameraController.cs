using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia
{
    public class BlankCameraController : ICameraController
    {
        public CameraControllerSettings settings;

        public void Init(CameraControllerSettings settings, Transform character, Camera camera)
        {
            this.settings = settings;
        }
        
        public void Update()
        {
            //Nothing
        }

    }   
}