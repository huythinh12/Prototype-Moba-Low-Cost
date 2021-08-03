using CharacterMechanism.Information;
using CharacterMechanism.ScriptableObject;
using CharacterMechanism.System;
using UnityEngine;

namespace CharacterMechanism.ScriptableObject
{
    [CreateAssetMenu(menuName = "CharacterMechanism/Example/ActionCondition/HasTargetInRangeAttack")]

    public class HasTargetInRangeAttack : AActionCondition
    {
        public override bool IsConditionFulfilled(CharacterSystem characterSystem, InputInformation inputInformation)
        {
            foreach (var characterTarget in characterSystem.GetTargetsDetecter.CharactersInDetectRange)
            {
                if (Vector3.Distance(characterSystem.transform.position, characterTarget.transform.position) <= characterSystem.GetProfile.RangeAttack)
                {
                    return true;
                }
            }

            return false;
        }
    }
}