using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

public class HeroInventory : MonoBehaviour
{
    public HeroSlot heroSlot;

    // Start is called before the first frame update
    void Start()
    {
        AddCharacterToListUI();
    }

    private void AddCharacterToListUI()
    {
        List<CharacterSystem> allCharacters = new List<CharacterSystem>();
        foreach (var character in CharacterSystemDatabase.Instance.Database)
        {
            if(character.Value.GetProfile.GetTypeCharacter == TypeCharacter.Hero)
            allCharacters.Add(character.Value);
        }

        for (int i = 0; i < allCharacters.Count; i++)
        {
           var objHero = Instantiate(heroSlot, transform);
           objHero.AddHero(allCharacters[i]);
        }
    }

}
