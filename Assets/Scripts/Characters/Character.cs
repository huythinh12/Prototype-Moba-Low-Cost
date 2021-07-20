using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TypeCharacter
{
    Unset,
    Hero,
    Legion,
    SmallCreep,
    MediumCreep,
    LargeCreep,
    Tower,
}

public enum Team
{
    Unset,
    Blue,
    Red,
    Natural,
}

public class Character : MonoBehaviour
{
    [Header("Information Character")]
    public TypeCharacter typeCharacter;
    public Team team;

    [Header("Stast")]
    public StatsCharacter stats;
    public string Name { get; set; }
    public string ID { get; private set; }


    public event Action<Health> OnHealthChanged;
    public event Action<Mana> OnManaChanged;
    public event Action<Level> OnLevelChanged;


    private void Awake()
    {
        StatsBar.AddFor(this, typeCharacter, team);
    }

    private void Start()
    {
        stats.SetDefault();

        OnHealthChanged?.Invoke(stats.health);
        OnManaChanged?.Invoke(stats.mana);
        OnLevelChanged?.Invoke(stats.level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stats.health.Current -= 10;
            OnHealthChanged?.Invoke(stats.health);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            stats.mana.Current -= 10;
            OnManaChanged?.Invoke(stats.mana);
        }
    }


    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
    }
}
