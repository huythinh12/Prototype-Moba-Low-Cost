using System.Collections.Generic;
using UnityEngine;
using CharacterMechanism.System;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> spawnPointHeroBlue;
    [SerializeField]
    private List<Transform> spawnPointHeroRed;
    private DataSelected dataSelected;
    private CharacterSystem characterSystem;
    [SerializeField]
    List<CharacterSpawner> characterSpawners = new List<CharacterSpawner>();
    Dictionary<CharacterSystem, Vector3> spawnPointOfCharacters = new Dictionary<CharacterSystem, Vector3>();

    void Start()
    {
        dataSelected = FindObjectOfType<DataSelected>();
        if(dataSelected)
            SpawnHeroDefault();

    }

    private void SpawnHeroDefault()
    {
        foreach (var characterSpawner in dataSelected.characterDataPersistence)
        {
            CharacterSystem characterSystemSpawned = CharacterSystem.Create(characterSpawner.nameID, characterSpawner.teamCharacter, characterSpawner.typeBehavior);
            SetSpawnPoint(characterSystemSpawned);
            characterSystemSpawned.transform.position = GetSpawnPoint(characterSystemSpawned);
         
        } 
    }

  
    private void SetSpawnPoint(CharacterSystem characterSystem)
    {
        spawnPointOfCharacters.Add(characterSystem, GetSpawnPointDontUse(characterSystem.GetProfile.GetTeamCharacter));
    }

    public Vector3 GetSpawnPoint(CharacterSystem characterSystem)
    {
        if (spawnPointOfCharacters.ContainsKey(characterSystem))
        {
            Vector3 spawnPoint;
            spawnPointOfCharacters.TryGetValue(characterSystem, out spawnPoint);
            return spawnPoint;
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 GetSpawnPointDontUse(TeamCharacter teamCharacter)
    {
        Vector3 spawnPointDontUse = Vector3.zero;

        switch (teamCharacter)
        {
            case TeamCharacter.Natural:
                break;
            case TeamCharacter.Blue:
                if (spawnPointHeroBlue != null)
                {
                    spawnPointDontUse = spawnPointHeroBlue[0].position;
                    spawnPointHeroBlue.Remove(spawnPointHeroBlue[0]);
                }
                break;
            case TeamCharacter.Red:
                if (spawnPointHeroRed != null)
                {
                    spawnPointDontUse = spawnPointHeroRed[0].position;
                    spawnPointHeroRed.Remove(spawnPointHeroRed[0]);
                }
                break;
            default:
                spawnPointDontUse = Vector3.zero;
                break;
        }

        return spawnPointDontUse;
    }
}