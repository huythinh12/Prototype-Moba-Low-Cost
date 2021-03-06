using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.UI;
using TMPro;
using CharacterMechanism.System;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;
    public BattleDiaglog battleDiaglog;

    public Stopwatch BattleTime = new Stopwatch();
    [HideInInspector] public int ScoreBlue = 0;
    [HideInInspector] public int ScoreRed = 0;

    [SerializeField] TextMeshProUGUI BattleTimeText;
    [SerializeField] TextMeshProUGUI ScoreBlueText;
    [SerializeField] TextMeshProUGUI ScoreRedText;
    //[HideInInspector] public KDAUI kdaPlayerUI;

    public List<CharacterSystem> RedTowers;
    public List<CharacterSystem> BlueTowers;


    void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        BattleTime.Start();
        InvokeRepeating("UpdateBattleTimeText", 0f, 1f);
    }

    void UpdateBattleTimeText()
    {
        BattleTimeText.text = string.Format("{0:00}:{1:00}", BattleTime.Elapsed.Minutes, BattleTime.Elapsed.Seconds);
    }

    public void UpdateScoreTexts(CharacterSystem characterDie)
    {
        //if (characterDie.information.typeCharacter == TypeCharacter.Hero)
        //{
        //    if (characterDie.team == TeamCharacter.Blue)
        //    {
        //        ScoreRed++;
        //        ScoreRedText.text = ScoreRed.ToString();
        //    }
        //    else if (characterDie.team == TeamCharacter.Red)
        //    {
        //        ScoreBlue++;
        //        ScoreBlueText.text = ScoreBlue.ToString();
        //    }

        //    kdaPlayerUI.UpdateKDAText();
        //}
    }


}
