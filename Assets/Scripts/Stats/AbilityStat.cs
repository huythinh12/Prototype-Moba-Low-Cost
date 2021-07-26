using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class AbilityStat
{
    [SerializeField] protected float baseValue;
    [SerializeField] protected float perLevel;

    protected float value;
    protected int Level = 1;

    public float BaseValue { get => baseValue; set => baseValue = value; }
    public float PerLevel { get => perLevel; set => perLevel = value; }

    public virtual float Value
    {
        get
        {
            value = BaseValue + Level * PerLevel;
            return value;
        }
    }

    public AbilityStat(float baseValue, float perLevel)
    {
        this.baseValue = baseValue;
        this.perLevel = perLevel;
    }

    public AbilityStat() : this(0f)
    {
    }

    public AbilityStat(float baseValue) : this(baseValue, 0f) { }


    public void SubscribeOnLevelChange(Character character)
    {
        character.OnLevelChanged += SetLevel;
    }

    public void SetLevel(int level)
    {
        this.Level = level;
    }

}
