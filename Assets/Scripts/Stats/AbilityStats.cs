using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AbilityStats
{
    public static readonly int MinLevel = 1;
    public static readonly int MaxLevel = 6;

    [SerializeField] int level = 1;

    AbilityStat manaCost = new AbilityStat();
    AbilityStat cooldownTime = new AbilityStat();
    AbilityStat effectDuartion;
    AbilityStat castRangeMax = new AbilityStat();
    AbilityStat castRangeMin;
    AbilityStat castDelayTime;
    AbilityStat widthAreaOfEffect = new AbilityStat();
    AbilityStat heightAreaOfEffect;
    AbilityStat physicalDamageBase;
    AbilityStat physicalDamageFactor;
    AbilityStat magicDamageBase;
    AbilityStat magicDamageFactor;

    public AbilityStat ManaCost { get => manaCost; set => manaCost = value; }
    public AbilityStat CooldownTime { get => cooldownTime; set => cooldownTime = value; }
    public AbilityStat EffectDuartion { get => effectDuartion; set => effectDuartion = value; }
    public AbilityStat CastRangeMax { get => castRangeMax; set => castRangeMax = value; }
    public AbilityStat CastRangeMin { get => castRangeMin; set => castRangeMin = value; }
    public AbilityStat CastDelayTime { get => castDelayTime; set => castDelayTime = value; }
    public AbilityStat WidthAreaOfEffect { get => widthAreaOfEffect; set => widthAreaOfEffect = value; }
    public AbilityStat HeightAreaOfEffect { get => heightAreaOfEffect; set => heightAreaOfEffect = value; }
    public AbilityStat PhysicalDamageBase { get => physicalDamageBase; set => physicalDamageBase = value; }
    public AbilityStat PhysicalDamageFactor { get => physicalDamageFactor; set => physicalDamageFactor = value; }
    public AbilityStat MagicDamageBase { get => magicDamageBase; set => magicDamageBase = value; }
    public AbilityStat MagicDamageFactor { get => magicDamageFactor; set => magicDamageFactor = value; }
}
