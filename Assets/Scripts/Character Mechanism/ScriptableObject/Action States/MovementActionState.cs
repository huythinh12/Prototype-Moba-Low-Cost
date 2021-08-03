using System.Collections;
using System.Collections.Generic;
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
    [CreateAssetMenu(menuName = "CharacterMechanism/Example/ActionState/Movement")]
    public sealed class MovementActionState : AActionState
    {
        public override void BeginAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
        }

        public override void EndAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            characterSystem.GetRigidbody.velocity = Vector3.zero;

            characterSystem.GetAnimator.SetBool("isMove", false);
            characterSystem.GetAnimator.SetFloat("movementSpeed", 0f);
        }

        public override void UpdateAction(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            characterSystem.transform.LookAt(characterSystem.transform.position + inputInformation.MovementDirection);
            characterSystem.GetRigidbody.velocity = inputInformation.MovementDirection * characterSystem.GetProfile.MovementSpeed;


            if (characterSystem.GetRigidbody.velocity == Vector3.zero)
            {
                characterSystem.GetAnimator.SetBool("isMove", false);
            }
            else
            {
                characterSystem.GetAnimator.SetBool("isMove", true);
                characterSystem.GetAnimator.SetFloat("movementSpeed", characterSystem.GetProfile.MovementSpeed / 3f);
            }
        }
    }
}
