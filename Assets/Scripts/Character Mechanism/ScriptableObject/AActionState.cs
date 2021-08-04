using CharacterMechanism.Information;
using CharacterMechanism.System;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    /// <inheritdoc/>
    /// <summary>
    /// Base class to create an action state
    /// </summary>
    public abstract class AActionState : UnityEngine.ScriptableObject
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [SerializeField] public ActionTransition[] actionTransitions = null;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        /////////////////////////
        ////////// API //////////

        /// <summary>
        /// Attempt to return the next action state
        /// </summary>
        public AActionState AttemptToGetNextActionState(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            foreach (var actionTransition in this.actionTransitions)
            {
                var nextActionState = actionTransition.Simulate(characterSystem, inputInformation);

                if (nextActionState)
                {
                    return (nextActionState);
                }
            }
            return (null);
        }

        //////////////////////////////
        ////////// Callback //////////

        /// <summary>
        /// Launch the action
        /// </summary>
        /// <remarks>
        /// Call when the action state is loaded
        /// </remarks>
        public abstract void BeginAction(CharacterSystem characterSystem, InputInformation inputInformation);

        /// <summary>
        /// Close the action
        /// </summary>
        /// <remarks>
        /// Call when the action state is changed
        /// </remarks>
        public abstract void EndAction(CharacterSystem characterSystem, InputInformation inputInformation);

        /// <summary>
        /// Update the action
        /// </summary>
        /// <remarks>
        /// Call every FixedUpdate
        /// </remarks>
        public abstract void UpdateAction(CharacterSystem characterSystem, InputInformation inputInformation);
    }
}

