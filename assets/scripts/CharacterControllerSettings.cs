using UnityEngine;

namespace Bloktopia
{
    [System.Serializable]
    public class CharacterControllerSettings
    {
        /// <summary>
        /// The maximum translation force
        /// </summary>
        public float maxMovementForce = 10f;

        /// <summary>
        /// The jump speed
        /// </summary>
        public float jumpImpulseSpeed = 10f;

        /// <summary>
        /// The turn speed
        /// </summary>
        public float turnSpeed = 10f;

        /// <summary>
        /// Should we freeze roll and pitch rotation?
        /// </summary>
        public bool freezeRollPitchRotation = true;
    }
}