using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CharacterMechanism.System;

public class ImpactWithCamera : BaseAction
{
    private float percentageZoom;
    private float duration;

    public ImpactWithCamera(AbilityActionData data) : base(data)
    {
        percentageZoom = this.data.FloatFields.Find((a) => a.Name == "percentageZoom").Value;
        duration = this.data.FloatFields.Find((a) => a.Name == "duration").Value;
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, CharacterSystem selfCharacter, CharacterSystem targetCharacter)
    {
        float fieldOfViewBegin = Camera.main.fieldOfView;
        Camera.main.DOFieldOfView(fieldOfViewBegin * percentageZoom, 0.75f);

        yield return new WaitForSeconds(duration);
        Camera.main.DOFieldOfView(fieldOfViewBegin, 1f);
    }

    public override BaseAction Clone()
    {
        return new ImpactWithCamera(data);
    }
}
