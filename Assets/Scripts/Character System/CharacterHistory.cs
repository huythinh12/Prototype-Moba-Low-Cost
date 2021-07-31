using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

struct InformationCharacterHit
{
    public Character characterHit;
    public DateTime timeHit;
    public float damage;

    public InformationCharacterHit(Character characterHit, float damage)
    {
        this.characterHit = characterHit;
        this.damage = damage;
        this.timeHit = DateTime.Now;
    }
}

public class CharacterHistory
{
    static readonly float KillFactor = 1.75f;
    static readonly float AssistFactor = 1.25f;
    static readonly float DeathFactor = 0.75f;
    static readonly TimeSpan LifetimeHistoryCharacterHit = new TimeSpan(0, 0, 10);
    static readonly int MaxCapacityHistoryCharactersHit = 5;

    public int Kill = 0;
    public int Death = 0;
    public int Assist = 0;

    private int amountHeroKilledDiscontinuity = 0;
    private int amountHeroKilledContinual = 0;
    private TimeSpan LastTimeKillHero;

    public int AmountHeroKilledDiscontinuity { get { return amountHeroKilledDiscontinuity; } }
    public int AmountHeroKilledContinual
    {
        get
        {
            if (GameManager.Instance.BattleTime.Elapsed.TotalSeconds - LastTimeKillHero.TotalSeconds <= LifetimeHistoryCharacterHit.TotalSeconds)
            {
                return amountHeroKilledContinual;
            }
            else
            {
                amountHeroKilledContinual = 0;
                return amountHeroKilledContinual;
            }
        }
    }

    public void Reset()
    {
        amountHeroKilledDiscontinuity = 0;
        amountHeroKilledContinual = 0;
        HistoryCharactersHit.Clear();
    }

    public void AddHeroKilled(Character character)
    {
        amountHeroKilledDiscontinuity++;

        if (amountHeroKilledContinual == 0)
        {
            amountHeroKilledContinual++;
        }
        else if (GameManager.Instance.BattleTime.Elapsed.TotalSeconds - LastTimeKillHero.TotalSeconds <= LifetimeHistoryCharacterHit.TotalSeconds)
        {
            amountHeroKilledContinual++;
        }
        else
        {
            amountHeroKilledContinual = 0;
        }

        LastTimeKillHero = new TimeSpan(0, 0, (int)GameManager.Instance.BattleTime.Elapsed.TotalSeconds);
    }


    public float Score
    {
        get
        {
            return (Kill * KillFactor + Assist * AssistFactor) / (Death * DeathFactor);
        }
    }

    List<InformationCharacterHit> HistoryCharactersHit;

    Character self;

    public CharacterHistory(Character character)
    {
        this.self = character;
        HistoryCharactersHit = new List<InformationCharacterHit>(MaxCapacityHistoryCharactersHit);
    }

    //public InformationCharacterHit GetCharacterLastHit()
    //{
    //    RemoveHistoryWhenEndLifetime();
    //    SortHistoryBasedOnTimeHit();

    //    int indexCharacterLastHit = 0;
    //    return HistoryCharactersHit[indexCharacterLastHit];
    //}

    public Character GetCharacterKill()
    {
        return HistoryCharactersHit[0].characterHit;
    }


    public void AddHistoryCharacterHit(Character characterHit, float damage)
    {
        Debug.Log(string.Format("Add {0} in History Character Hit!", characterHit));

        HistoryCharactersHit.Add(new InformationCharacterHit(characterHit, damage));


        if (IsExistInHistory(characterHit))
        {

        }

        //RemoveHistoryWhenEndLifetime();
        //SortHistoryBasedOnTimeHit();
    }

    public void HandleKDA()
    {
        RemoveHistoryWhenEndLifetime();
        SortHistoryBasedOnTimeHit();

        this.Death++;

        for (int i = 0; i < HistoryCharactersHit.Count; i++)
        {
            if (i == 0)
            {
                Character killer = HistoryCharactersHit[i].characterHit;
                killer.History.Kill++;
                killer.History.AddHeroKilled(self);
            }
            else
            {
                //HistoryCharactersHit[i].characterHit.History.Assist++;
            }
        }
    }


    private void RemoveHistoryWhenEndLifetime()
    {
        foreach (var characterHit in HistoryCharactersHit)
        {
            if (DateTime.Now - characterHit.timeHit > LifetimeHistoryCharacterHit)
            {
                HistoryCharactersHit.Remove(characterHit);
            }
        }
    }

    private void SortHistoryBasedOnTimeHit()
    {
        HistoryCharactersHit.Sort(CompareTimeHit);
    }

    private int CompareTimeHit(InformationCharacterHit characterX, InformationCharacterHit characterY)
    {
        if (characterX.timeHit < characterY.timeHit)
        {
            return -1;
        }
        else if (characterX.timeHit > characterY.timeHit)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private bool IsExistInHistory(Character character)
    {
        foreach (var charactersHit in HistoryCharactersHit)
        {
            if (charactersHit.characterHit == character)
            {
                return true;
            }
        }

        return false;
    }
}
