using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoahUltimateAbility : Ability
{
    public NoahUltimateAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Self;
        this.classifyAbility = ClassifyAbility.Ultimate;

        this.cooldownTimeCurrent = 5f;
        this.castRangeMaxCurrent = 0f;
        this.castRangeMinCurrent = 0f;
        this.castDelayTimeCurrent = 0f;
        this.widthAreaOfEffectCurrent = 0f;
        this.heightAreaOfEffectCurrent = 0f;

    }

    public override void UseAblity(Character self, Vector3 indicator)
    {
        self.HealingHealth(0, 0.15f);
    }
}
