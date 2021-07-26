using UnityEngine;

public class NoahAlphaAbility : Ability
{
    float speedUp = 0.3f;

    public NoahAlphaAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Self;
        this.classifyAbility = ClassifyAbility.Alpha;

        Stats.CooldownTime = new AbilityStat(0.1f);
        Stats.CastRangeMax = new AbilityStat(0f);
        Stats.CastDelayTime = new AbilityStat(0f);
        Stats.EffectDuartion = new AbilityStat(2f);
        Stats.ManaCost = new AbilityStat(10f);

    }

    public override void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {
        self.StartCoroutine(self.Stats.MovementSpeed.AddModifierInTimeUnit(new StatModifier(speedUp, StatModifyType.PercentAdd), Stats.EffectDuartion.Value));
    }
}
