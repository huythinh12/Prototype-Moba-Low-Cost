using UnityEngine;

public class RecallAbility : Ability
{
    public RecallAbility()
    {
        this.indicatorAbilityType = IndicatorAbilityType.Self;
        this.classifyAbility = ClassifyAbility.Recall;

        Stats.CooldownTime = new AbilityStat(0.1f);
    }

    public override void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {
        self.Revival();
    }
}
