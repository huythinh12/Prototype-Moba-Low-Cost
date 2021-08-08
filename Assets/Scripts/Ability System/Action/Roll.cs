using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CharacterMechanism.System;

public class Roll : BaseAction
{
    bool isDirectionNormalized;
    float duration;

    public Roll(AbilityActionData data) : base(data)
    {
        isDirectionNormalized = this.data.BoolFields.Find((a) => a.Name == "isDirectionNormalized").Value;
        duration = this.data.FloatFields.Find((a) => a.Name == "duration").Value;
    }

    public override BaseAction Clone()
    {
        return new Roll(data);
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, CharacterSystem selfCharacter, CharacterSystem targetCharacter)
    {
        Vector3 direction = (isDirectionNormalized ? indicator.normalized : indicator) * owner.abilityData.RangeCast.Value / 2;
  
        //selfCharacter.isCanMove = false;

        //selfCharacter.transform.DOLookAt(selfCharacter.transform.position + indicator, 0.1f);

        //selfCharacter.rigidbody.DOMove(selfCharacter.transform.position + direction, duration).SetEase(Ease.InQuad);

        //selfCharacter.animator.SetBool("isRollEnd", false);
        //selfCharacter.animator.SetTrigger("Roll");

        yield return new WaitForSeconds(duration);
        //selfCharacter.animator.SetBool("isRollEnd", true);

        //selfCharacter.isCanMove = true;
    }
}
