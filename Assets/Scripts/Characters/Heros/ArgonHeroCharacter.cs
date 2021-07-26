using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgonHeroCharacter : Character
{
    protected override void AddAbility()
    {
        abilities.Add(new ArgonAlphaAbility());
    }
}
