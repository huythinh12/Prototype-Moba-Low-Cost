//using System.Collections.Generic;
//using UnityEngine;

//[DefaultExecutionOrder(100)]
//public class CharacterDatabase : MonoBehaviour
//{
//    [SerializeField]
//    public Dictionary<string, Character> Database = new Dictionary<string, Character>();
//    public static CharacterDatabase Instance { get; private set; }

//    void Awake()
//    {
//        if (Instance != null)
//        {
//            Destroy(gameObject);
//        }
//        else
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }

//        LoadCharacterData();
//    }

//    void LoadCharacterData()
//    {
//        var characters = Resources.LoadAll<DataDrivenCharacter>("Characters") as DataDrivenCharacter[];

//        for (int i = 0; i < characters.Length; i++)
//        {
//            DataDrivenCharacter rawCharacter = characters[i];
//            Character character = DataDrivenCharacter.Parse(rawCharacter);
//            Database[character.information.name] = character;
//        }
//    }

//    public Character GetCharacter(string characterName)
//    {
//        if (Database.ContainsKey(characterName))
//        {
//            return Database[characterName].Clone() as Character;
//        }

//        throw new System.Exception("Character: " + characterName + " doesn't exist");
//    }


//}
