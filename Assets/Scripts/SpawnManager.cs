using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using CharacterMechanism.System;
using System;

[DefaultExecutionOrder(150)]
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private static readonly int AmountLegionSpawnForTurn = 3;
    private static readonly float WaitingTimeStartSpawnFirstLegionTurn = 5f;
    private static readonly float SumTimeSpawnLegionForTurn = 6.5f;
    private static readonly float WaitingTimeNextTurn = 30f;

    [SerializeField] bool isSpawnDebug;

    [SerializeField]
    private List<Transform> spawnPointHeroBlue;
    [SerializeField]
    private List<Transform> spawnPointHeroRed;
    [SerializeField]
    private Transform positionUltimateTowerBlue;
    [SerializeField]
    private Transform positionUltimateTowerRed;

    private DataSelected dataSelected;
    private CharacterSystem characterSystem;
    [SerializeField]
    List<CharacterSpawner> characterSpawners = new List<CharacterSpawner>();
    Dictionary<CharacterSystem, Vector3> spawnPointOfCharacters = new Dictionary<CharacterSystem, Vector3>();

    private void Awake()
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

        if (isSpawnDebug)
        {
            gameObject.AddComponent<CharacterSystemDatabase>();
        }
    }


    void Start()
    {
        if (isSpawnDebug)
        {
            SpawnHeroDebug();
        }
        else
        {
            dataSelected = FindObjectOfType<DataSelected>();
            if (dataSelected)
                SpawnHeroDefault();
        }

        StartCoroutine(SpawnTurnsLegionFootman(TeamCharacter.Blue, AmountLegionSpawnForTurn, WaitingTimeStartSpawnFirstLegionTurn, SumTimeSpawnLegionForTurn, WaitingTimeNextTurn));
        StartCoroutine(SpawnTurnsLegionFootman(TeamCharacter.Red, AmountLegionSpawnForTurn, WaitingTimeStartSpawnFirstLegionTurn, SumTimeSpawnLegionForTurn, WaitingTimeNextTurn));
    }

    public Transform GetTransformUltimateTowerTarget(TeamCharacter teamCharacter)
    {
        switch (teamCharacter)
        {
            case TeamCharacter.Blue:
                return positionUltimateTowerRed;
            case TeamCharacter.Red:
                return positionUltimateTowerBlue;
            default:
                return positionUltimateTowerRed;
        }
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

    private void SpawnHeroDebug()
    {
        foreach (var characterSpawner in characterSpawners)
        {
            CharacterSystem characterSystemSpawned = CharacterSystem.Create(characterSpawner.nameID, characterSpawner.teamCharacter, characterSpawner.typeBehavior);
            SetSpawnPoint(characterSystemSpawned);
            characterSystemSpawned.transform.position = GetSpawnPoint(characterSystemSpawned);
        }
    }


    private void SetSpawnPoint(CharacterSystem characterSystem)
    {
        switch (characterSystem.GetProfile.GetTypeCharacter)
        {
            case TypeCharacter.Hero:
                spawnPointOfCharacters.Add(characterSystem, GetSpawnPointDontUse(characterSystem.GetProfile.GetTeamCharacter));
                break;
            case TypeCharacter.Legion:
                {
                    switch (characterSystem.GetProfile.GetTeamCharacter)
                    {
                        case TeamCharacter.Blue:
                            spawnPointOfCharacters.Add(characterSystem, positionUltimateTowerBlue.transform.position);
                            break;
                        case TeamCharacter.Red:
                            spawnPointOfCharacters.Add(characterSystem, positionUltimateTowerRed.transform.position);
                            break;
                    }

                    break;
                }
            case TypeCharacter.Tower:
                break;
            case TypeCharacter.SmallCreep:
                break;
            case TypeCharacter.MediumCreep:
                break;
            case TypeCharacter.LargeCreep:
                break;
            default:
                break;
        }
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

    private IEnumerator SpawnTurnsLegionFootman(TeamCharacter teamCharacter, int amountLegion, float waitingTimeStartSpawn, float sumTimeSpawn, float waitingTimeNextTurn)
    {
        WaitForSeconds waitingStartSpawn = new WaitForSeconds(waitingTimeStartSpawn);
        WaitForSeconds timeSpawnForEach = new WaitForSeconds(sumTimeSpawn / amountLegion);
        WaitForSeconds waitingNextTurn = new WaitForSeconds(waitingTimeNextTurn);
        string nameIDLegion = teamCharacter == TeamCharacter.Blue ? "Footman Blue" : "Footman Red";

        yield return waitingStartSpawn;

        while (true)
        {
            for (int i = 0; i < amountLegion; i++)
            {
                CharacterSystem characterSystemSpawned = CharacterSystem.Create(nameIDLegion, teamCharacter, TypeBehavior.Computer);
                SetSpawnPoint(characterSystemSpawned);
                characterSystemSpawned.transform.position = GetSpawnPoint(characterSystemSpawned);

                yield return timeSpawnForEach;
            }

            yield return waitingNextTurn;
        }
    }
}