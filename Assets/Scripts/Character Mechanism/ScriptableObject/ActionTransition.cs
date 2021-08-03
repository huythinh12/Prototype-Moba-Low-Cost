using CharacterMechanism.System;
using CharacterMechanism.Information;
using System.Linq;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    /// <inheritdoc/>
    /// <summary>
    /// ScriptableObject used to create action transition
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterMechanism/ScriptableObject/ActionTransition")]
    public class ActionTransition : UnityEngine.ScriptableObject
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [SerializeField] private AActionCondition[] actionConditions = null;
        [SerializeField] private AActionState actionStateOnSuccess = null;
        [SerializeField] private AActionState actionStateOnFailure = null;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        /// <summary>
        /// Simulate the transition in order to get the resulting action state
        /// </summary>
        public AActionState Simulate(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            if (this.actionConditions.All(ac => ac.IsConditionFulfilled(characterSystem, inputInformation)))
            {
                return (this.actionStateOnSuccess);
            }
            return (this.actionStateOnFailure);
        }
    }

}