using System;
using UnityEngine;
using CharacterMechanism.DataBase;

namespace CharacterMechanism.System
{
    /// <summary>
    /// Class containing all the locomotion settings
    /// </summary>
    [Serializable]
    public sealed class Profile
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        ////////////////////////////////////////
        ////// Get Data Form Profile Data //////

        private string name;
        private string description;
        private Sprite iconNormal;
        private Sprite iconMinimap;

        private TypeCharacter type;
        private HeroClass mainClass;
        private HeroClass subClass;

        private string attackAbilityName;
        private string alphaAbilityName;
        private string betaAbilityName;
        private string ultimateAbilityName;

        private TeamCharacter team;

        private CharacterStat healthMax;
        private CharacterStat manaMax;
        private CharacterStat physicalDamage;
        private CharacterStat magicDamage;
        private CharacterStat physicalDefense;
        private CharacterStat magicDefense;
        private CharacterStat physicalPierce;
        private CharacterStat magicPierce;
        private CharacterStat cooldownAttackk;
        private CharacterStat cooldownAbility;
        private CharacterStat criticalRate;
        private CharacterStat criticalDamage;
        private CharacterStat movementSpeed;
        private CharacterStat effectResistance;
        private CharacterStat damageDealFactor;
        private CharacterStat damageTakenFactor;
        private CharacterStat healingFactor;
        private CharacterStat rangeAttack;

        ////////////////////////////////////////
        /////////// Custom In Battle ///////////


        public static float DefenseConstant = 600f;
        public static float MinDefenseAffterPierce = 0f;
        public static float MinFinalPercentDamageTaken = 0f;

        public static readonly int MinLevel = 1;
        public static readonly int MaxLevel = 15;

        AttackType attackType;

        [SerializeField] int level = 1;
        float healthCurrent = 1;
        float manaCurrent = 1;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        public TeamCharacter GetTeamCharacter { get => team; }
        public TypeCharacter GetTypeCharacter { get => type; }
        public CharacterStat HealthMax { get => healthMax; }
        public CharacterStat ManaMax { get => manaMax; }
        public CharacterStat PhysicalDamage { get => physicalDamage; }
        public CharacterStat MagicDamage { get => magicDamage; }
        public CharacterStat PhysicalDefense { get => physicalDefense; }
        public CharacterStat MagicDefense { get => magicDefense; }
        public CharacterStat PhysicalPierce { get => physicalPierce; }
        public CharacterStat MagicPierce { get => magicPierce; }
        public CharacterStat CooldownAttackk { get => cooldownAttackk; }
        public CharacterStat CooldownAbility { get => cooldownAbility; }
        public CharacterStat CriticalRate { get => criticalRate; }
        public CharacterStat CriticalDamage { get => criticalDamage; }
        public CharacterStat MovementSpeed { get => movementSpeed; }
        public CharacterStat EffectResistance { get => effectResistance; }
        public CharacterStat DamageDealFactor { get => damageDealFactor; }
        public CharacterStat DamageTakenFactor { get => damageTakenFactor; }
        public CharacterStat HealingFactor { get => healingFactor; }
        public CharacterStat RangeAttack { get => rangeAttack; }

        public Profile(ProfileData profileData)
        {
            this.name = profileData.Name;
            this.description = profileData.Description;
            this.iconNormal = profileData.IconNormal;
            this.iconMinimap = profileData.IconMinimap;
            this.mainClass = profileData.MainClass;
            this.subClass = profileData.SubClass;
            this.attackAbilityName = profileData.AttackAbilityName;
            this.alphaAbilityName = profileData.AlphaAbilityName;
            this.betaAbilityName = profileData.BetaAbilityName;
            this.ultimateAbilityName = profileData.UltimateAbilityName;

            this.healthMax = new CharacterStat(profileData.HealthMax.BaseValue, profileData.HealthMax.PerLevelValue);
            this.manaMax = new CharacterStat(profileData.ManaMax.BaseValue, profileData.ManaMax.PerLevelValue);
            this.physicalDamage = new CharacterStat(profileData.PhysicalDamage.BaseValue, profileData.PhysicalDamage.PerLevelValue);
            this.magicDamage = new CharacterStat(profileData.PhysicalDefense.BaseValue, profileData.PhysicalDefense.PerLevelValue);
            this.physicalDefense = new CharacterStat(profileData.MagicDamage.BaseValue, profileData.MagicDamage.PerLevelValue);
            this.magicDefense = new CharacterStat(profileData.MagicDefense.BaseValue, profileData.MagicDefense.PerLevelValue);
            this.physicalPierce = new CharacterStat(profileData.PhysicalPierce.BaseValue, profileData.PhysicalPierce.PerLevelValue);
            this.magicPierce = new CharacterStat(profileData.MagicPierce.BaseValue, profileData.MagicPierce.PerLevelValue);
            this.cooldownAttackk = new CharacterStat(profileData.CooldownAttackk.BaseValue, profileData.CooldownAttackk.PerLevelValue);
            this.cooldownAbility = new CharacterStat(profileData.CooldownAbility.BaseValue, profileData.CooldownAbility.PerLevelValue);
            this.criticalRate = new CharacterStat(profileData.CriticalRate.BaseValue, profileData.CriticalRate.PerLevelValue);
            this.criticalDamage = new CharacterStat(profileData.CriticalDamage.BaseValue, profileData.CriticalDamage.PerLevelValue);
            this.movementSpeed = new CharacterStat(profileData.MovementSpeed.BaseValue, profileData.MovementSpeed.PerLevelValue);
            this.effectResistance = new CharacterStat(profileData.EffectResistance.BaseValue, profileData.EffectResistance.PerLevelValue);
            this.damageDealFactor = new CharacterStat(profileData.DamageDealFactor.BaseValue, profileData.DamageDealFactor.PerLevelValue);
            this.damageTakenFactor = new CharacterStat(profileData.DamageTakenFactor.BaseValue, profileData.DamageTakenFactor.PerLevelValue);
            this.healingFactor = new CharacterStat(profileData.HealingFactor.BaseValue, profileData.HealingFactor.PerLevelValue);
            this.rangeAttack = new CharacterStat(profileData.RangeAttack.BaseValue, profileData.RangeAttack.PerLevelValue);
        }

        public void SetTeam(TeamCharacter team)
        {
            this.team = team;
        }
    }
}