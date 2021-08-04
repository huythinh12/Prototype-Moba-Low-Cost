using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.DataBase;
using CharacterMechanism.System;

[CreateAssetMenu(fileName = "Data", menuName = "Character System/New", order = 1)]
public class DataDrivenCharacterSystem : ScriptableObject
{
    //[MenuItem("Battle/Character System/Create New")]
    //public static void CreateCharacter()
    //{
    //    AssetUtility.CreateAsset<DataDrivenCharacter>("Characters");
    //}

    [SerializeField]
    ProfileData profileData;
    [SerializeField]
    CharacterSound characterSound;

    public DataDrivenCharacterSystem()
    {

    }

    public static CharacterSystem Parse(DataDrivenCharacterSystem rawCharacterSystem)
    {
        CharacterSystem characterSystem = new CharacterSystem(rawCharacterSystem.profileData);
        return characterSystem;
    }
}
