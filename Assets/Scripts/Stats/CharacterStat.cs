using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class CharacterStat
{
    [SerializeField] protected float baseValue;
    [SerializeField] protected float perLevel;

    protected float value;
    protected int Level = 1;
    protected bool isModified = true;

    protected readonly List<StatModifier> statModifiers;

    public float BaseValue { get => baseValue; set => baseValue = value; }
    public float PerLevel { get => perLevel; set => perLevel = value; }


    public virtual float Value
    {
        get
        {
            if (isModified)
            {
                value = CalculateFinalValue();
            }

            return value;
        }
    }

    public readonly ReadOnlyCollection<StatModifier> StatModifiers;


    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue, float perLevel) : this()
    {
        this.baseValue = baseValue;
        this.perLevel = perLevel;
    }

    public CharacterStat(float baseValue) : this(baseValue, 0f) { }


    public virtual void AddModifier(StatModifier modifier)
    {
        isModified = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifierOrder);
    }

    /// <summary>
    /// Use StartCoroutine to run this method
    /// </summary>
    public virtual IEnumerator AddModifierInTimeUnit(StatModifier modifier, float timeUnit)
    {
        AddModifier(modifier);

        yield return new WaitForSeconds(timeUnit);
        RemoveModifier(modifier);
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {
        if (statModifiers.Remove(modifier))
        {
            isModified = true;
            return true;
        }

        return false;
    }

    public virtual bool RemoveAllModierFromSource(object source)
    {
        bool didRemove = false;

        // "for" loop in reverse to improve performance
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isModified = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier x, StatModifier y)
    {
        if (x.Order > y.Order)
        {
            return 1;
        }
        else if (x.Order < y.Order)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    protected virtual float CalculateFinalValue()
    {
        isModified = false;

        float finalValue = BaseValue + PerLevel * Level;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            switch (statModifiers[i].Type)
            {
                case StatModifyType.Flat:
                    {
                        finalValue += statModifiers[i].Value;
                        break;
                    }

                case StatModifyType.PercentAdd:
                    {
                        sumPercentAdd += statModifiers[i].Value;

                        int nextIndex = i + 1;
                        if (nextIndex >= statModifiers.Count || statModifiers[nextIndex].Type != StatModifyType.PercentAdd)
                        {
                            finalValue *= 1 + sumPercentAdd;
                        }

                        break;
                    }
                case StatModifyType.PercentMulti:
                    {
                        finalValue *= 1 + statModifiers[i].Value;
                        break;
                    }
            }
        }

        return (float)Math.Round(finalValue, 2);
    }


    public float GetValuePercent(float percent)
    {
        return Value * percent;
    }

    public void SubscribeOnLevelChange(Character character)
    {
        character.OnLevelChanged += SetLevel;
    }

    public void SetLevel(int level)
    {
        isModified = true;
        this.Level = level;
    }

}
