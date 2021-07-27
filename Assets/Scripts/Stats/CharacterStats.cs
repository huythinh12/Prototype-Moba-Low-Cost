using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterStats : MonoBehaviour
{
    public static float DefenseConstant = 600f;
    public static float MinDefenseAffterPierce = 0f;
    public static float MinFinalPercentDamageTaken = 0f;

    public static readonly int MinLevel = 1;
    public static readonly int MaxLevel = 15;

    AttackType attackType;

    [SerializeField] int level = 1;
    float healthCurrent = 1;
    float manaCurrent = 1;

    [SerializeField] CharacterStat healthMax = new CharacterStat(100f);
    [SerializeField] CharacterStat manaMax = new CharacterStat(100f);
    [SerializeField] CharacterStat physicalDamage = new CharacterStat();
    [SerializeField] CharacterStat magicDamage = new CharacterStat();
    [SerializeField] CharacterStat physicalDefense = new CharacterStat();
    [SerializeField] CharacterStat magicDefense = new CharacterStat();
    [SerializeField] CharacterStat physicalPierce = new CharacterStat();
    [SerializeField] CharacterStat magicPierce = new CharacterStat();
    [SerializeField] CharacterStat cooldownAttackk = new CharacterStat();
    [SerializeField] CharacterStat cooldownAbility = new CharacterStat();
    [SerializeField] CharacterStat criticalRate = new CharacterStat();
    [SerializeField] CharacterStat criticalDamage = new CharacterStat();
    [SerializeField] CharacterStat movementSpeed = new CharacterStat(3f);
    [SerializeField] CharacterStat effectResistance = new CharacterStat();
    [SerializeField] CharacterStat damageDealFactor = new CharacterStat(1f);
    [SerializeField] CharacterStat damageTakenFactor = new CharacterStat(1f);
    [SerializeField] CharacterStat heaingFactor = new CharacterStat(1f);
    [SerializeField] CharacterStat rangeAttack = new CharacterStat(2f);


    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            // Edit: Gioi han Min - Max
            level = Mathf.Clamp(value, MinLevel, MaxLevel);
        }
    }
    public float HealthCurrent
    {
        get
        {
            return healthCurrent;
        }

        set
        {
            // Edit: Rut gon code
            healthCurrent = Mathf.Clamp(value, 0, HealthMax.Value);
        }
    }
    public float ManaCurrent
    {
        get
        {
            return manaCurrent;
        }

        set
        {
            // Edit: Rut gon code
            manaCurrent = Mathf.Clamp(value, 0, ManaMax.Value);
        }
    }

    public CharacterStat HealthMax { get => healthMax; set => healthMax = value; }
    public CharacterStat ManaMax { get => manaMax; set => manaMax = value; }
    public CharacterStat PhysicalDamage { get => physicalDamage; set => physicalDamage = value; }
    public CharacterStat MagicDamage { get => magicDamage; set => magicDamage = value; }
    public CharacterStat PhysicalDefense { get => physicalDefense; set => physicalDefense = value; }
    public CharacterStat MagicDefense { get => magicDefense; set => magicDefense = value; }
    public CharacterStat PhysicalPierce { get => physicalPierce; set => physicalPierce = value; }
    public CharacterStat MagicPierce { get => magicPierce; set => magicPierce = value; }
    public CharacterStat CooldownAttackk { get => cooldownAttackk; set => cooldownAttackk = value; }
    public CharacterStat CooldownAbility { get => cooldownAbility; set => cooldownAbility = value; }
    public CharacterStat CriticalRate { get => criticalRate; set => criticalRate = value; }
    public CharacterStat CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    public CharacterStat MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public CharacterStat EffectResistance { get => effectResistance; set => effectResistance = value; }
    public CharacterStat DamageDealFactor { get => damageDealFactor; set => damageDealFactor = value; }
    public CharacterStat DamageTakenFactor { get => damageTakenFactor; set => damageTakenFactor = value; }
    public CharacterStat HeaingFactor { get => heaingFactor; set => heaingFactor = value; }
    public CharacterStat RangeAttack { get => rangeAttack; set => rangeAttack = value; }

    public void Start()
    {
        SubscribeOnLevelChange(GetComponent<Character>());
    }

    public void ResetHealthCurrent()
    {
        HealthCurrent = HealthMax.Value;
    }

    public void ResetManaCurrent()
    {
        ManaCurrent = ManaMax.Value;
    }

    public void SubscribeOnLevelChange(Character character)
    {
        HealthMax.SubscribeOnLevelChange(character);
        ManaMax.SubscribeOnLevelChange(character);
        physicalDamage.SubscribeOnLevelChange(character);
        magicDamage.SubscribeOnLevelChange(character);
        physicalDefense.SubscribeOnLevelChange(character);
        magicDefense.SubscribeOnLevelChange(character);
        physicalPierce.SubscribeOnLevelChange(character);
        magicPierce.SubscribeOnLevelChange(character);
        cooldownAttackk.SubscribeOnLevelChange(character);
        cooldownAbility.SubscribeOnLevelChange(character);
        criticalRate.SubscribeOnLevelChange(character);
        criticalDamage.SubscribeOnLevelChange(character);
        movementSpeed.SubscribeOnLevelChange(character);
        effectResistance.SubscribeOnLevelChange(character);
        damageDealFactor.SubscribeOnLevelChange(character);
        damageTakenFactor.SubscribeOnLevelChange(character);
        heaingFactor.SubscribeOnLevelChange(character);
        rangeAttack.SubscribeOnLevelChange(character);
    }

    public static float GetFinalPercentDamageTaken(DamageType damageType, Character characterDealDamage, Character characterTakeDamage)
    {
        float beginPercent = 1f;

        float defense = 0f;
        float pierce = 0f;

        switch (damageType)
        {
            case DamageType.Physical:
                {
                    defense = characterTakeDamage.Stats.PhysicalDefense.Value;
                    pierce = characterDealDamage.Stats.PhysicalPierce.Value;
                    break;
                }
            case DamageType.Magic:
                {
                    defense = characterTakeDamage.Stats.MagicDefense.Value;
                    pierce = characterDealDamage.Stats.MagicPierce.Value;
                    break;
                }
        }

        float defenseAffterPierce = defense - pierce;
        defenseAffterPierce = defenseAffterPierce < MinDefenseAffterPierce ? MinDefenseAffterPierce : defenseAffterPierce;
        float defensePercent = defenseAffterPierce / (defenseAffterPierce + DefenseConstant);

        float takenDamageFactor = characterTakeDamage.Stats.DamageTakenFactor.Value;
        float dealDamageFactor = characterDealDamage.Stats.DamageDealFactor.Value;
        float reduceDamagePercent = takenDamageFactor / dealDamageFactor;

        float finalPercentDamageTaken = (beginPercent - (defensePercent)) * reduceDamagePercent;
        finalPercentDamageTaken = finalPercentDamageTaken < MinFinalPercentDamageTaken ? MinFinalPercentDamageTaken : finalPercentDamageTaken;

        return finalPercentDamageTaken;
    }

    public float GetPercentHealth(float percentMaxHealth, StatPercentType statPercentType)
    {
        switch (statPercentType)
        {
            case StatPercentType.Max:
                return percentMaxHealth * HealthMax.Value;
            case StatPercentType.Current:
                return percentMaxHealth * HealthCurrent;
            case StatPercentType.Lost:
                return percentMaxHealth * (HealthMax.Value - HealthCurrent);
            default:
                return 0f;
        }
    }
}
