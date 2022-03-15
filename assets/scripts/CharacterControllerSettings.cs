using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Bloktopia
{
    [System.Serializable]
    public class CharacterControllerSettings
    {
        [Header("Movement settings ")]

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
        /// Whether or not to apply motion in the direction of the camera
        /// </summary>
        // [Tooltip("If this is toggled, movement forward is directed in the direction the camera is pointed")]
        // public bool applyCameraForward = true;


        /// <summary>
        /// Should we freeze roll and pitch rotation?
        /// </summary>
        public bool freezeRollPitchRotation = true;

        [Header("Animation settings ")]

        /// <summary>
        /// The animator for the player
        /// </summary>
        public Animator playerAnimator;

        /// <summary>
        /// The variable to change to the movement speed
        /// </summary>
        public string animationMovementVariable = "walkSpeed";

        /// <summary>
        /// The trigger for attacking
        /// </summary>
        public string animationAttackTriggerVariable = "smash";

        /// <summary>
        /// The trigger for jumping
        /// </summary>
        public string jumpAnimationTriggerVariable = "jump";


        /// <summary>
        /// A list of animation states in which movement should not be updated in
        /// </summary>
        /// <value></value>
        public List<string> immovableMovementClips = new List<string> { "Smash", "Jump" };
    }
}