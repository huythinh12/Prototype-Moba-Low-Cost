using CharacterMechanism.Information;
using CharacterMechanism.System;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    /// <inheritdoc/>
    /// <summary>
    /// Example of action condition for a movement direction detection
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterMechanism/Example/ActionCondition/HasNoMovementDirection")]
    public sealed class HasNoMovementDirectionActionCondition : AActionCondition
    {
        public override bool IsConditionFulfilled(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            return inputInformation.MovementDirection == Vector3.zero;
        }
    }
}
