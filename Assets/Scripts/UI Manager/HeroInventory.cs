using System.Collections.Generic;
using UnityEngine;

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
        List<Character> allCharacters = new List<Character>();
        foreach (var character in CharacterDatabase.Instance.Database)
        {
            allCharacters.Add(character.Value);
        }

        for (int i = 0; i < allCharacters.Count; i++)
        {
           var objHero = Instantiate(heroSlot, transform);
           objHero.AddHero(allCharacters[i]);
        }
    }

}
