using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;

[DefaultExecutionOrder(100)]
public class CharacterSystemDatabase : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, CharacterSystem> Database = new Dictionary<string, CharacterSystem>();
    public static CharacterSystemDatabase Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        LoadCharacterData();
    }

    void LoadCharacterData()
    {
        var characters = Resources.LoadAll<DataDrivenCharacterSystem>("Characters") as DataDrivenCharacterSystem[];

        for (int i = 0; i < characters.Length; i++)
        {
            DataDrivenCharacterSystem rawCharacterSystem = characters[i];
            CharacterSystem character = DataDrivenCharacterSystem.Parse(rawCharacterSystem);
            Database[character.GetProfile.Name] = character;

            Debug.Log(string.Format("Data Base: Loaded {0}", character.GetProfile.Name));
        }
    }

    public CharacterSystem GetCharacter(string characterSystemName)
    {
        if (Database.ContainsKey(characterSystemName))
        {
            CharacterSystem characterSystem = Database[characterSystemName].Clone() as CharacterSystem;
            return characterSystem;
        }

        throw new System.Exception("Character: " + characterSystemName + " doesn't exist");
    }

}
