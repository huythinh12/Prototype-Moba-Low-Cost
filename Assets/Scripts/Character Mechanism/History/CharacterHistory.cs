using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CharacterMechanism.Attribute;
using CharacterMechanism.System;

namespace CharacterMechanism.History
{
    struct InformationCharacterHit
    {
        ////////////////////////////
        ///////// Attribute ////////
        ////////////////////////////

        public CharacterSystem characterHit;
        public DateTime timeHit;
        public float damage;

        ////////////////////////////
        //////// Constructor ///////
        ////////////////////////////

        public InformationCharacterHit(CharacterSystem characterHit, float damage)
        {
            this.characterHit = characterHit;
            this.damage = damage;
            this.timeHit = DateTime.Now;
        }

        public InformationCharacterHit(CharacterSystem characterHit) : this(characterHit, 0f) { }
    }

    [Serializable]
    public class CharacterHistory
    {
        ////////////////////////////
        ///////// Attribute ////////
        ////////////////////////////

        ///////////////////////////////
        /////////// General ///////////

        static readonly int MaxCapacityHistoryCharactersHit = 5;
        static readonly float TimeBetweenMultiKills = 10;

        ///////////////////////////////
        /////////// KDA Stat //////////

        [ReadOnly] int kill;
        [ReadOnly] int deadth;
        [ReadOnly] int assist;

        ///////////////////////////////
        ////////// Kill Stat //////////

        [ReadOnly] int amountHeroKilledDiscontinuity = 0;
        [ReadOnly] int amountHeroKilledContinual = 0;
        [ReadOnly] DateTime LastTimeKillHero;

        ////////////////////////////
        ///////// Property /////////
        ////////////////////////////

        //////////////////////////////////////
        /////// History Character Hit ////////

        List<InformationCharacterHit> HistoryCharactersHit = new List<InformationCharacterHit>();

        ///////////////////////////////
        /////////// KDA Stat //////////

        public int Kill { get => kill; set => kill = value; }
        public int Deadth { get => deadth; set => deadth = value; }
        public int Assit { get => assist; set => assist = value; }

        ///////////////////////////////
        ////////// Kill Stat //////////

        public int AmountHeroKilledDiscontinuity
        {
            get
            {
                return amountHeroKilledDiscontinuity;
            }

            private set
            {
                amountHeroKilledDiscontinuity = value;
            }
        }

        public int AmountHeroKilledContinual
        {
            get
            {
                return amountHeroKilledContinual;
            }

            private set
            {
                if (amountHeroKilledContinual < 1 || IsDontPassTimeBetweenMultiKills(LastTimeKillHero))
                {
                    amountHeroKilledContinual++;
                }
                else
                {
                    amountHeroKilledContinual = 1;
                }

                LastTimeKillHero = DateTime.Now;
            }
        }

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        /////////////////////////////
        //////////// API ////////////

        /// <summary>
        /// 
        /// </summary>
        static public void HandleKDAWhenDie(CharacterSystem characterSystem)
        {
            characterSystem.GetHistory.Deadth++;

            CharacterSystem characterLastHit = characterSystem.GetHistory.GetCharacterLastHit();
            List<CharacterSystem> assitCharacters = characterSystem.GetHistory.GetCharacterAssit();

            characterLastHit.GetHistory.AddCharacterKilled(characterSystem);
            foreach (var character in assitCharacters)
            {
                character.GetHistory.Assit++;
            }
        }

        /// <summary>
        /// Return true if Datime LastHit and Now do not exceed 10 seconds
        /// </summary>
        static bool IsDontPassTimeBetweenMultiKills(DateTime dateTimeLastHit)
        {
            return (DateTime.Now - dateTimeLastHit).TotalSeconds <= TimeBetweenMultiKills;
        }

        /// <summary>
        /// Set amount hero killed in before session to 0
        /// </summary>
        public void Reset()
        {
            HistoryCharactersHit.Clear();
            amountHeroKilledDiscontinuity = 0;
            amountHeroKilledContinual = 0;
        }

        /// <summary>
        /// Add information character hit in list InformationCharacterHit
        /// </summary>
        public void AddCharacterHit(CharacterSystem characterSystem)
        {
            HistoryCharactersHit.Add(new InformationCharacterHit(characterSystem));
        }

        /// <summary>
        /// Update KDA and Kill stat when this character kill other enemy hero
        /// </summary>
        public void AddCharacterKilled(CharacterSystem characterSystem)
        {
            AmountHeroKilledDiscontinuity++;
            AmountHeroKilledContinual++;
        }

        /// <summary>
        /// 
        /// </summary>
        public CharacterSystem GetCharacterLastHit()
        {
            InformationCharacterHit informationCharacterLastHit = HistoryCharactersHit[0];

            for (int i = 0; i < HistoryCharactersHit.Count; i++)
            {
                if (informationCharacterLastHit.timeHit < HistoryCharactersHit[i].timeHit)
                {
                    informationCharacterLastHit = HistoryCharactersHit[i];
                }
            }

            return informationCharacterLastHit.characterHit;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<CharacterSystem> GetCharacterAssit()
        {
            CharacterSystem characterLastHit = GetCharacterLastHit();
            List<CharacterSystem> assitCharacters = new List<CharacterSystem>();

            for (int i = 0; i < HistoryCharactersHit.Count; i++)
            {
                if (HistoryCharactersHit[i].characterHit != characterLastHit && IsDontPassTimeBetweenMultiKills(HistoryCharactersHit[i].timeHit))
                {
                    assitCharacters.Add(HistoryCharactersHit[i].characterHit);
                }
            }

            return assitCharacters;
        }
    }
}