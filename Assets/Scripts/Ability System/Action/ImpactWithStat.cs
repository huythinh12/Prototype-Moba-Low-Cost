using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactWithStat : BaseAction
{
    private TypeStat typeStat;

    private float amountIncrease;
    private float percentageIncrease;

    private float duration;

    public ImpactWithStat(AbilityActionData data) : base(data)
    {
        typeStat = this.data.TypeStats;

        amountIncrease = this.data.FloatFields.Find((a) => a.Name == "amountIncrease").Value;
        percentageIncrease = this.data.FloatFields.Find((a) => a.Name == "percentageIncrease").Value;

        duration = this.data.FloatFields.Find((a) => a.Name == "duration").Value;
    }

    public override IEnumerator Excecute(Ability owner, Vector3 indicator, Character selfCharacter, Character targetCharacter)
    {
        StatModifier amountStatModifier = new StatModifier(amountIncrease, StatModifyType.Flat);
        StatModifier percentageStatModifier = new StatModifier(percentageIncrease, StatModifyType.PercentAdd);

        switch (typeStat)
        {   
            case TypeStat.RangeAttack:
                selfCharacter.StartCoroutine(selfCharacter.Stats.RangeAttack.AddModifierInTimeUnit(amountStatModifier, duration));
                selfCharacter.StartCoroutine(selfCharacter.Stats.RangeAttack.AddModifierInTimeUnit(percentageStatModifier, duration));
                break;
            case TypeStat.MovementSpeed:
                selfCharacter.StartCoroutine(selfCharacter.Stats.MovementSpeed.AddModifierInTimeUnit(amountStatModifier, duration));
                selfCharacter.StartCoroutine(selfCharacter.Stats.MovementSpeed.AddModifierInTimeUnit(percentageStatModifier, duration));
                break;
            case TypeStat.PhysicalDamage:
                selfCharacter.StartCoroutine(selfCharacter.Stats.PhysicalDamage.AddModifierInTimeUnit(amountStatModifier, duration));
                selfCharacter.StartCoroutine(selfCharacter.Stats.PhysicalDamage.AddModifierInTimeUnit(percentageStatModifier, duration));
                break;
            case TypeStat.MagicDamage:
                selfCharacter.StartCoroutine(selfCharacter.Stats.MagicDamage.AddModifierInTimeUnit(amountStatModifier, duration));
                selfCharacter.StartCoroutine(selfCharacter.Stats.MagicDamage.AddModifierInTimeUnit(percentageStatModifier, duration));
                break;
            default:
                break;
        }


        yield return null;
    }

    public override BaseAction Clone()
    {
        return new ImpactWithStat(data);
    }

}
