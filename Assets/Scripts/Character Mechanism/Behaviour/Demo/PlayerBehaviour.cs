using System.Collections;
using System.Collections.Generic;
using CharacterMechanism.Information;
using UnityEngine;

namespace CharacterMechanism.Behaviour
{
    /// <inheritdoc/>
    /// <summary>
    /// Example of player behaviour using the generic character behaviour
    /// </summary>
    /// <remarks>
    /// ACharacterBehaviour is used because the script doesn't need collision and trigger detection
    /// </remarks>
    public sealed class PlayerBehaviour : ACharacterBehaviour
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [Header("Input Setting")]
        [SerializeField] private MovementJoystick movementJoystick = null;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////
        ////////// Callback //////////

        ////////// Activation //////////

        protected override void OnDestroy()
        { }

        protected override void OnDisable()
        { }

        protected override void OnEnable()
        { }

        ////////// Input Information //////////

        protected override void InitializeInformation()
        { }

        protected override void LoadInformationComponents()
        { }

        protected override void OverrideInputInformationReset(InputInformation inputInformation)
        { }

        protected override void UpdateInputInformation(InputInformation inputInformation)
        {
            inputInformation.MovementDirection = movementJoystick.GetDirectionXZ;
        }
    }
}
