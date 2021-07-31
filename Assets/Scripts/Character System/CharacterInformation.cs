using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterInformation
{
    public string name;
    public string description;
    public string id;

    public TypeCharacter typeCharacter;


    public HeroClass mainClass;
    public HeroClass subClass;

    public Sprite icon;

    public string AlphaAbilityName;
    public string BetaAbilityName;
    public string UltimateAbilityName;
}
