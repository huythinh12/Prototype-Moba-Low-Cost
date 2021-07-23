using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum IndicatorAbilityType
{
    Self,
    OnlyRange,
    Circle,
    Cone,
    Rectangle,
    Line,
}


public enum ClassifyAbility
{
    Unset,
    Alpha,
    Beta,
    Ultimate
}

public enum ButtonPosition
{
    Hide,
    Alpha,
    Beta,
    Ultimate,
}

public enum BodyPartPoint
{
    Head,
    Chest,
    LeftHand,
    RightHand,
    LeftLeg,
    RightLeg,
}

[System.Serializable]
public class Ability
{
    // Text
    protected new string name;
    protected string description;

    // Art & Sound Region
    protected Image iconNormal;
    protected ButtonPosition buttonPosition;
    protected Dictionary<BodyPartPoint, ParticleSystem> targetEffectAttachmentPoints;
    protected AudioClip soundEffect;

    // Stat Region
    protected int manaCostCurrent;
    protected int manaCostBase;
    protected int manaCostPerLevel;

    public float cooldownTimeCurrent;
    protected float cooldownTimeBase;
    protected float cooldownTimePerLevel;

    protected float[] effectDurationTimeCurrent;
    protected float[] effectDurationTimeBase;
    protected float[] effectDurationTimePerLevel;
    protected Dictionary<TypeCharacter, float[]> effectDurationTimeFactorForCharacters;

    // Cast Stat Region
    public IndicatorAbilityType indicatorAbilityType;
    public ClassifyAbility classifyAbility;

    public float castRangeMaxCurrent;
    public float castRangeMaxBase;
    public float castRangePerMaxLevel;

    protected float castRangeMinCurrent;
    protected float castRangeMinBase;
    protected float castRangePerMinLevel;

    protected float castDelayTimeCurrent;
    protected float castDelayTimeBase;
    protected float castDelayTimePerLevel;

    public float widthAreaOfEffectCurrent;
    public float widthAreaOfEffectBase;
    public float widthAreaOfEffectLevel;

    public float heightAreaOfEffectCurrent;
    public float heightAreaOfEffectBase;
    public float heightAreaOfEffectLevel;

    // Battle Stat Region
    protected float[] physicalDamageBase;
    protected float[] physicalDamagePerLevel;
    protected float[] physicalDamageFactor;
    protected float[] physicalDamageFactorPerLevel;

    // Missile Stat Region
    protected GameObject prefabMissile;
    protected float speedMissile;
    protected BodyPartPoint targetPointMissile;


    public virtual void UseAblity(Character self, Vector3 indicatorXZ)
    {

    }
}
