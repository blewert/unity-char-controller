using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia
{
    public class FixedLockCameraController : ICameraController
    {
        public CameraControllerSettings settings;
        private Transform character;
        private Camera camera;

        public void Init(CameraControllerSettings settings, Transform character, Camera camera)
        {
            this.settings = settings;
            this.character = character;
            this.camera = camera;
        }

        public void Update()
        {
            //Set camera position
            camera.transform.position = character.position + (settings.offset.normalized * settings.distance);
        }

    }
}