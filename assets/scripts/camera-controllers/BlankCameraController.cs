using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia.CameraController
{
    public class BlankCameraController : ICameraController
    {
        public Settings settings;

        public void Init(in Settings settings)
        {
            this.settings = settings;
        }
        
        public void Update()
        {
            //Something else 
            Debug.Log("Update called in BlankCameraController.");
        }

    }   
}