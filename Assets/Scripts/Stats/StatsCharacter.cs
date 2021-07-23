using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsCharacter : MonoBehaviour
{
    public Level level;
    public Health health;
    public Mana mana;
    public MagicDamage magicDamage;
    public MagicDefense magicDefense;
    public PhysicalDamage physicalDamage;
    public PhysicalDefense physicalDefense;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDefault()
    {
        level.Current = level.Start;

        health.Reset(level.Current);
        mana.Reset(level.Current);

        physicalDamage.Current = physicalDamage.Start + physicalDamage.PerLevel * level.Current;
    }

    public void ResetHealth()
    {
        health.Max = health.Start + health.PerLevel * level.Current;
        health.Current = health.Max;
    }

    public void ResetMana()
    {
        mana.Max = mana.Start + mana.PerLevel * level.Current;
        mana.Current = health.Max;
    }
}
