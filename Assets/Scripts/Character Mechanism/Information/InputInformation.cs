using System;
using UnityEngine;
using CharacterMechanism.Attribute;

namespace CharacterMechanism.Information
{
    /// <inheritdoc/>
    /// <summary>
    /// Class containing all the standard input information
    /// </summary>
    [Serializable]
    public class InputInformation : IInformation
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [ReadOnly] public Vector3 MovementDirection = Vector3.zero;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        public void Reset()
        {
            this.MovementDirection.Set(0f, 0f, 0f);
        }
    }
}


