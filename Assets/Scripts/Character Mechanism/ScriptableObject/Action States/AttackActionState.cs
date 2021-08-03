using CharacterMechanism.Information;
using CharacterMechanism.ScriptableObject;
using CharacterMechanism.System;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    [CreateAssetMenu(menuName = "CharacterMechanism/Example/ActionState/Attack")]
    public sealed class AttackActionState : AActionState
    {
        public override void BeginAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            characterSystem.GetAnimator.SetBool("isAttack", true);
        }

        public override void EndAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            characterSystem.GetAnimator.SetBool("isAttack", false);
        }

        public override void UpdateAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            Debug.Log("Attack Ation State");
        }
    }
}