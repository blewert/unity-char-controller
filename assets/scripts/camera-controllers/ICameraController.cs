using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia
{
    public interface ICameraController
    {
        public void Init(CameraControllerSettings settings, Transform character, Camera camera);

        public void Update();
    }
}