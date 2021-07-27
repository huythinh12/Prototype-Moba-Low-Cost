using UnityEngine;

public class ArgonAlphaAbility : Ability
{
    float jumpDuration = 1f;

    public ArgonAlphaAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Circle;
        this.classifyAbility = ClassifyAbility.Alpha;

        Stats.CooldownTime = new AbilityStat(7f);
        Stats.CastRangeMax = new AbilityStat(40f);
        Stats.CastDelayTime = new AbilityStat(0f);
        Stats.WidthAreaOfEffect = new AbilityStat(5f);
        Stats.ManaCost = new AbilityStat(35f);

    }

    public override void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {
        Vector3 direction = indicatorXZ * Stats.CastRangeMax.Value / 2;
        self.JumpTo(direction, jumpDuration);
    }
}
