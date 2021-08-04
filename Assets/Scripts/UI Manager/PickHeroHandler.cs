using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using CharacterMechanism.System;

public class PickHeroHandler : MonoBehaviour
{
    float waitingTime = 60f;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI nameHeroText;
    [SerializeField] Button readyButton;
    [SerializeField] GameObject panelInformationTeam;
    [SerializeField] GameObject panelHeroInventory;
    [SerializeField] GameObject[] informationBackground;
    [SerializeField] GameObject backgroundMainPlayer;

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
            if (item.GetComponent<TextMeshProUGUI>())
            {
                DataSelected.Instance.nameHero.Add(characterSystem.GetProfile.Name);
                item.GetComponent<TextMeshProUGUI>().text = characterSystem.GetProfile.Name;
            }
            else if (item.transform.name.StartsWith("Avatar"))
            {
                item.GetComponent<Image>().sprite = characterSystem.GetProfile.IconNormal;
            }
        }
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
       
        RandomPickHeroAI(allCharacters,1);
        RandomPickHeroAI(allCharacters, 2);

        // set interactable UI false when tap ready button
        foreach (Transform item in panelHeroInventory.transform)
        {
            item.gameObject.GetComponent<Button>().interactable = false;
        }

        waitingTime = 11;
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
