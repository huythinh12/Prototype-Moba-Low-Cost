using UnityEngine;

public class NoahBetaAbility : Ability
{
    public NoahBetaAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Rectangle;
        this.classifyAbility = ClassifyAbility.Beta;

        Stats.CooldownTime = new AbilityStat(8f);
        Stats.CastRangeMax = new AbilityStat(10f);
        Stats.CastDelayTime = new AbilityStat(0f);
        Stats.WidthAreaOfEffect = new AbilityStat(1.15f);
        Stats.EffectDuartion = new AbilityStat(0.75f);
        Stats.ManaCost = new AbilityStat(75f);
    }

    public override void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {
        Vector3 direction = JoystickMath.OxzIndicatorHalfNormalized(indicatorXZ) * Stats.CastRangeMax.Value;
        self.Glide(direction, Stats.EffectDuartion.Value);
    }
}
