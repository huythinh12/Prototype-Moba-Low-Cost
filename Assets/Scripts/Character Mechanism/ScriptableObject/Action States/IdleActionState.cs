using CharacterMechanism.Information;
using CharacterMechanism.ScriptableObject;
using CharacterMechanism.System;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    /// <inheritdoc/>
    /// <summary>
    /// Example of idle action state 
    /// </summary>
    [CreateAssetMenu(menuName = "CharacterMechanism/Example/ActionState/Idle")]
    public sealed class IdleActionState : AActionState
    {
        public override void BeginAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            
        }

        public override void EndAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
        }

        public override void UpdateAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
        }
    }
}