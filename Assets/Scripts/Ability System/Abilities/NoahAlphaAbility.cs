using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahAlphaAbility : Ability
{
    public NoahAlphaAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Line;
        this.classifyAbility = ClassifyAbility.Alpha;

        this.cooldownTimeCurrent = 5f;
        this.castRangeMaxCurrent = 35f;
        this.castRangeMinCurrent = 0f;
        this.castDelayTimeCurrent = 0f;
        this.widthAreaOfEffectCurrent = 3f;
        this.heightAreaOfEffectCurrent = 3f;

    }

    public override void UseAblity(Character self, Vector3 indicator)
    {
        self.HealingHealth(0, 0.15f);
    }
}
