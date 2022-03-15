using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bloktopia.CameraController
{
    public interface ICameraController
    {
        public void Init(in Settings settings);

        public void Update();
    }
}