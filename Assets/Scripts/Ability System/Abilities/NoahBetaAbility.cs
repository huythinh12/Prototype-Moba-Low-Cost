using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahBetaAbility : Ability
{
    public NoahBetaAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Rectangle;
        this.classifyAbility = ClassifyAbility.Beta;

        this.cooldownTimeCurrent = 5f;
        this.castRangeMaxCurrent = 40f;
        this.castRangeMinCurrent = 0f;
        this.castDelayTimeCurrent = 0f;
        this.widthAreaOfEffectCurrent = 1.75f;
        this.heightAreaOfEffectCurrent = 0f;

        this.effectDurationTimeCurrent = new float[1];
        this.effectDurationTimeCurrent[0] = 1.25f;

    }

    public override void UseAblity(Character self, Vector3 indicatorXZ)
    {
        Vector3 direction = JoystickMath.OxzIndicatorHalfNormalized(indicatorXZ) * castRangeMaxCurrent;
        self.Glide(direction, effectDurationTimeCurrent[0]);
    }
}
