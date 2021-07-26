using UnityEngine;

public class NoahUltimateAbility : Ability
{
    AbilityStat percentLostHealth = new AbilityStat(0.24f);


    public NoahUltimateAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Self;
        this.classifyAbility = ClassifyAbility.Ultimate;

        Stats.CooldownTime = new AbilityStat(5.0f);
        Stats.CastRangeMax = new AbilityStat(0f);
        Stats.CastDelayTime = new AbilityStat(0f);
        Stats.ManaCost = new AbilityStat(75);

    }

    public override void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {
        self.HealingHealth(percentLostHealth.Value, StatPercentType.Lost);
    }
}
