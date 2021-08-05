using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using CharacterMechanism.System;


public class PickHeroHandler : MonoBehaviour
{
    //UI
    [SerializeField] float waitingTime = 60f;
    [SerializeField] float lastWaiting = 15f;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI nameHeroText;
    [SerializeField] Button readyButton;
    // Data Reference from outside 
    [SerializeField] GameObject panelInformationTeam;
    [SerializeField] GameObject panelHeroInventory;
    [SerializeField] GameObject[] informationBackground;
    [SerializeField] GameObject backgroundMainPlayer;
    // Data spawner for scene battle
    private CharacterSpawner[] characterspawner = new CharacterSpawner[3];

    // Start is called before the first frame update
    void Start()
    {
        readyButton.onClick.AddListener(HandleReadyButton);
        InvokeRepeating("UpdateWaitingTime", 0f, 1f);
    }

    private void HandlePlayerSelected(string obj)
    {
        if (obj.Length > 0)
        {
            readyButton.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        HeroSlot.OnHeroNameSelected += HandlePlayerSelected;
        HeroSlot.OnHeroSelected += HandleSelectedHero;
    }

    private void HandleSelectedHero(GameObject iconUI, CharacterSystem characterSystem)
    {
        informationBackground[0].SetActive(true);
        SetMainCharacterToUI(characterSystem, 0);
    }

    private void SetMainCharacterToUI(CharacterSystem characterSystem, int index)
    {
        foreach (Transform item in informationBackground[index].transform)
        {
            //Add to character selected list base on condition
            if (index == 0)
            {
                AddToDataSelected(characterSystem, TeamCharacter.Blue, TypeBehavior.Player,index);
            }
            else if (index > 0)
            {
                AddToDataSelected(characterSystem, TeamCharacter.Blue, TypeBehavior.Computer, index);
            }

            // set info to UI 
            if (item.GetComponent<TextMeshProUGUI>())
            {
                item.GetComponent<TextMeshProUGUI>().text = characterSystem.GetProfile.Name;
            }
            else if (item.transform.name.StartsWith("Avatar"))
            {
                item.GetComponent<Image>().sprite = characterSystem.GetProfile.IconNormal;
            }
        }
    }

    private void AddToDataSelected(CharacterSystem characterSystem,TeamCharacter team, TypeBehavior typeBehavior,int index)
    {
        characterspawner[index] = new CharacterSpawner();
        characterspawner[index].nameID = characterSystem.GetProfile.Name;
        characterspawner[index].teamCharacter = team;
        characterspawner[index].typeBehavior = typeBehavior;
    }

    private void OnDisable()
    {
        HeroSlot.OnHeroNameSelected -= HandlePlayerSelected;
        HeroSlot.OnHeroSelected -= HandleSelectedHero;
    }

    private void HandleReadyButton()
    {
        //add list character data
        List<CharacterSystem> allCharacters = new List<CharacterSystem>();
        foreach (var character in CharacterSystemDatabase.Instance.Database)
        {
            allCharacters.Add(character.Value);
        }

        // pick HeroAI Random
        RandomPickHeroAI(allCharacters,1);
        RandomPickHeroAI(allCharacters,2);

        // set inter-able UI false when tap ready button
        foreach (Transform item in panelHeroInventory.transform)
        {
            item.gameObject.GetComponent<Button>().interactable = false;
        }

        //add characterData into list DataSelected when hit ready button
        foreach (var item in characterspawner)
        {
            DataSelected.Instance.characterDataPersistence.Add(item);
        } 

        // set variable when hit ready button
        waitingTime = lastWaiting;
        readyButton.interactable = false;
    }

    private void RandomPickHeroAI(List<CharacterSystem> allCharacters,int index)
    {
        var random = Random.Range(0, 6);
        for (int i = 0; i < allCharacters.Count; i++)
        {
            if (random == i)
            {
                informationBackground[index].SetActive(true);
                SetMainCharacterToUI(allCharacters[i], index);
                break;
            }
        }
    }

    void UpdateWaitingTime()
    {
        waitingTime--;
        timeText.text = waitingTime.ToString();

        if (waitingTime <= 0f)
        {
            SceneManager.LoadScene("Battle");
        }
    }

}
