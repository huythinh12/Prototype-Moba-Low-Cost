//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CharacterCombatController
//{
//    Character self;

//    public bool IsStuned;


//    //public void MoveToTarget(Character character)
//    //{
//    //    if (isAlive && CombatController.IsStuned == false)
//    //    {
//    //        Vector3 direction = character.transform.position - this.transform.position;
//    //        Move(direction.normalized * 0.85f);
//    //    }
//    //}


//    public CharacterCombatController(Character character)
//    {
//        this.self = character;
//    }


//    public void HealingHealth(float amount)
//    {
//        if (self.isAlive)
//        {
//            float healthHealed = amount;
//            self.Stats.HealthCurrent += healthHealed;

//            self.HandleEventHealingHealth(healthHealed);
//            self.HandleEventHealthChanged();
//        }
//    }

//    public void HealingHealth(float percentMaxHealth, StatPercentType statPercentType)
//    {
//        if (self.isAlive)
//        {
//            float healthHealed = self.Stats.GetPercentHealth(percentMaxHealth, statPercentType);
//            HealingHealth(healthHealed);
//        }
//    }

//    public void TakeDamage(Character characterDealDamage, DamageType damageType, float amountDamage)
//    {
//        if (self.isAlive)
//        {
//            float damageTaken = amountDamage * CharacterStats.GetFinalPercentDamageTaken(damageType, characterDealDamage, self);
//            damageTaken *= CharacterStats.GetFinalPercentDamageTaken(damageType, characterDealDamage, self);

//            self.Stats.HealthCurrent -= damageTaken;

//            self.HandleEventTakeDamage(damageTaken);

//            self.History.AddHistoryCharacterHit(characterDealDamage, damageTaken);
//            Debug.Log(string.Format("{0} take {1} {2} Damage form {3}", self.information.name, damageTaken, damageType.ToString(), characterDealDamage.information.name));
//        }
//    }

//    public void TakeDamage(Character characterDealDamage, DamageType damageType, float percentMaxHealth, StatPercentType statPercentType)
//    {
//        if (self.isAlive)
//        {
//            float damageTaken = self.Stats.GetPercentHealth(percentMaxHealth, statPercentType);
//            TakeDamage(characterDealDamage, damageType, damageTaken);
//        }
//    }

//    public bool UseMana(float mana)
//    {
//        if (self.Stats.ManaCurrent - mana >= 0)
//        {
//            self.Stats.ManaCurrent -= mana;
//            self.HandleEventManaChanged();
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    public void LevelUp()
//    {
//        self.Stats.Level++;
//        self.HandleEventLevelChanged();
//    }

//    public void HealingMana(float amount)
//    {
//        if (self.isAlive)
//        {
//            self.Stats.ManaCurrent += amount;
//            self.HandleEventManaChanged();
//        }
//    }
//}
