using UnityEngine;

public class NoahHeroCharacter : Character
{
    protected override void AddAbility()
    {
        abilities.Add(new NoahAlphaAbility());
        abilities.Add(new NoahBetaAbility());
        abilities.Add(new NoahUltimateAbility());
    }
}
