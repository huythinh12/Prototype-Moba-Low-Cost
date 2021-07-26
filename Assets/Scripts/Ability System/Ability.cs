using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Ability
{
    public AbilityStats Stats;

    // Text
    new protected string name;
    protected string description;
    protected Sprite icon;

    // Art & Sound Region
    protected ButtonPosition buttonPosition;
    protected Dictionary<BodyPartPoint, ParticleSystem> targetEffectAttachmentPoints;
    protected AudioClip soundEffect;

    protected Dictionary<TypeCharacter, float[]> effectDurationTimeFactorForCharacters;

    // Cast Stat Region
    public IndicatorAbilityType indicatorAbilityType;
    public ClassifyAbility classifyAbility;


    // Missile Stat Region
    protected GameObject prefabMissile;
    protected float speedMissile;
    protected BodyPartPoint targetPointMissile;

    protected bool isCooldowning = true;


    public Ability()
    {
        Stats = new AbilityStats();
    }


    public virtual bool UseAblity(Character self, Vector3 indicatorXZ)
    {
        if (self.UseMana(Stats.ManaCost.Value))
        {
            PerformBahaviors(self, indicatorXZ);
            return true;
        }

        return false;
    }

    public virtual void PerformBahaviors(Character self, Vector3 indicatorXZ)
    {

    }
}
