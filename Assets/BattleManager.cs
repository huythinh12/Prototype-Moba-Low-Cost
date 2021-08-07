using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using CharacterMechanism.System;
public class BattleManager : MonoBehaviour
{
    [SerializeField] Image victoryTextImage;    
    [SerializeField] GameObject panel;    
    [SerializeField] Image defeatTextImage;
    public static BattleManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        panel.gameObject.SetActive(false);
        victoryTextImage.gameObject.SetActive(false);
        defeatTextImage.gameObject.SetActive(false);
    }
    public void ClickToContinue()
    {
        SceneManager.LoadScene("Main Loading");
    }

    public void EndGame(CharacterSystem towerDie)
    {
        panel.gameObject.SetActive(true);

        switch (towerDie.GetProfile.GetTeamCharacter)
        {
            case TeamCharacter.Red:
                victoryTextImage.gameObject.SetActive(true);
                break;
            case TeamCharacter.Blue:
                defeatTextImage.gameObject.SetActive(true);
                break;
          
        }

    }
}
