using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class DataDrivenCharacter : ScriptableObject
{
    [MenuItem("Battle/Character/Create New")]
    public static void CreateCharacter()
    {
        AssetUtility.CreateAsset<DataDrivenCharacter>("Characters");
    }

    [SerializeField]
    CharacterInformation characterInformation;
    [SerializeField]
    CharacterStats characterStats;

    public DataDrivenCharacter()
    {
    }

    public static Character Parse(DataDrivenCharacter rawCharacter)
    {
        Character character = new Character(rawCharacter.characterInformation, rawCharacter.characterStats);
        return character;
    }
}
