using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.System;

public class LoaderBattle : MonoBehaviour
{
    public AvatarHeroLoading[] heroAvatar;
    public CharacterSystem characterSystem;

    // Start is called before the first frame update
    void Start()
    {
        characterSystem = FindObjectOfType<CharacterSystem>();
        if (DataSelected.Instance)
            AddCharacterToListUI(DataSelected.Instance.characterDataPersistence);
    }

    private void AddCharacterToListUI(List<CharacterSpawner> listHero)
    {
        // get list character system 
        List<CharacterSystem> allCharacters = new List<CharacterSystem>();
        foreach (var character in CharacterSystemDatabase.Instance.Database)
        {
            if (character.Value.GetProfile.GetTypeCharacter == TypeCharacter.Hero)
                allCharacters.Add(character.Value);
        }


        //set data for Avatar Hero UI 
        foreach (var character in allCharacters)
        {
            for (int i = 0; i < listHero.Count; i++)
            {

                heroAvatar[i].heroName.text = listHero[i].nameID;
                if (character.GetProfile.Name == listHero[i].nameID)
                    heroAvatar[i].GetComponent<Image>().sprite = character.GetProfile.ImageLoading;
            }
        }


    }

}
