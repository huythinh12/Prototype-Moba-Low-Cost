using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CharacterMechanism.DataBase
{
    [Serializable]
    public struct StatDataBase
    {
        public float BaseValue;
        public float PerLevelValue;

        public StatDataBase(float BaseValue, float PerLevelValue)
        {
            this.BaseValue = BaseValue;
            this.PerLevelValue = PerLevelValue;
        }

        public StatDataBase(float BaseValue) : this(BaseValue, 0)
        {
        }
    }

    /// <summary>
    /// Struct used to transfer data when initializing Character System
    /// </summary>
    [Serializable]
    public struct ProfileData
    {
        public string Name;
        public string Description;
        public Sprite IconNormal;
        public Sprite IconMinimap;
        public Sprite ImageLoading;

        public TypeCharacter TypeCharacter;
        public HeroClass MainClass;
        public HeroClass SubClass;

        public string AttackAbilityName;
        public string AlphaAbilityName;
        public string BetaAbilityName;
        public string UltimateAbilityName;

        public StatDataBase HealthMax;
        public StatDataBase ManaMax;
        public StatDataBase PhysicalDamage;
        public StatDataBase MagicDamage;
        public StatDataBase PhysicalDefense;
        public StatDataBase MagicDefense;
        public StatDataBase PhysicalPierce;
        public StatDataBase MagicPierce;
        public StatDataBase CooldownAttackk;
        public StatDataBase CooldownAbility;
        public StatDataBase CriticalRate;
        public StatDataBase CriticalDamage;
        public StatDataBase MovementSpeed;
        public StatDataBase EffectResistance;
        public StatDataBase DamageDealFactor;
        public StatDataBase DamageTakenFactor;
        public StatDataBase HealingFactor;
        public StatDataBase RangeAttack;

    }
}
