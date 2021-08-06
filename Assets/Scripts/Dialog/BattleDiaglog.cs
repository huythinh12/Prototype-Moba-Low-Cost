using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CharacterMechanism.System;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class BattleDiaglog : MonoBehaviour
{
    const string FristBlood = "First Blood";
    const string DoubleKill = "Double Kill";
    const string TripleKill = "Triple Kill";
    const string QuadraKill = "Quadra Kill";
    const string PentaKill = "Penta Kill";
    const string ShutDown = "Shut Down";
    const string Kill = "Kill";
    const string KillingSpree = "Killing Spree";
    const string Rampage = "Rampage";
    const string Unstoppable = "Unstoppable";
    const string Dominating = "Dominating";
    const string Godlike = "Godlike";
    const string Legendary = "Legendary";

    [SerializeField] Image iconKiller;
    [SerializeField] Image iconDeadman;
    [SerializeField] Image borderKiller;
    [SerializeField] Image borderDeadman;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI textKill;

    [Header("Sound Path"), Space]
    [SerializeField] string imagePathBorderBlue;
    [SerializeField] string imagePathBorderRed;
    [SerializeField] string imagePathBackgroundBlue;
    [SerializeField] string imagePathBackgroundRed;

    [Header("Sound Path"), Space]
    [SerializeField] string soundPath = "Sounds/Dialog/";

    private int sumKill = 0;
    private float timeHide = 0;

    private AudioSource audioSource;

    static public BattleDiaglog Instance;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    public static string GetTextKill(CharacterSystem characterKiller, CharacterSystem characterWasKilled, int sumKill)
    {
        if (sumKill == 1)
        {
            return FristBlood;
        }
        else if (characterKiller.GetHistory.AmountHeroKilledContinual >= 2)
        {
            switch (characterKiller.GetHistory.AmountHeroKilledContinual)
            {
                case 2:
                    return DoubleKill;
                case 3:
                    return TripleKill;
                case 4:
                    return QuadraKill;
                default:
                    return PentaKill;
            }
        }
        else if (characterWasKilled.GetHistory.AmountHeroKilledDiscontinuity >= 3)
        {
            return ShutDown;

        }
        else if (characterKiller.GetHistory.AmountHeroKilledDiscontinuity <= 2)
        {
            return Kill;
        }
        else if (characterKiller.GetHistory.AmountHeroKilledDiscontinuity >= 2)
        {
            switch (characterKiller.GetHistory.AmountHeroKilledDiscontinuity)
            {
                case 3:
                    return KillingSpree;
                case 4:
                    return Rampage;
                case 5:
                    return Unstoppable;
                case 6:
                    return Dominating;
                case 7:
                    return Godlike;
                default:
                    return Legendary;
            }
        }
        else
        {
            return "ERROR!";
        }
    }

    public void ShowKillDialog(CharacterSystem characterWasKilled)
    {
        if (characterWasKilled.GetProfile.GetTypeCharacter == TypeCharacter.Hero)
        {
            gameObject.SetActive(true);


            CharacterSystem characterKiller = characterWasKilled.GetHistory.GetCharacterLastHit();

            iconKiller.sprite = characterKiller.GetProfile.IconNormal;
            iconDeadman.sprite = characterWasKilled.GetProfile.IconNormal;

            switch (characterWasKilled.GetProfile.GetTeamCharacter)
            {
                case TeamCharacter.Blue:
                    background.sprite = Resources.Load<Sprite>(imagePathBackgroundRed);
                    borderDeadman.sprite = Resources.Load<Sprite>(imagePathBorderBlue);
                    borderKiller.sprite = Resources.Load<Sprite>(imagePathBorderRed);
                    break;
                case TeamCharacter.Natural:
                case TeamCharacter.Red:
                    background.sprite = Resources.Load<Sprite>(imagePathBackgroundBlue);
                    borderDeadman.sprite = Resources.Load<Sprite>(imagePathBorderRed);
                    borderKiller.sprite = Resources.Load<Sprite>(imagePathBorderBlue);
                    break;
            }

            sumKill++;

            string textDialog = BattleDiaglog.GetTextKill(characterKiller, characterWasKilled, sumKill);


            textKill.text = textDialog;
            AlignPosX(iconKiller, -GetAllainBasedOnTextKill(textDialog));
            AlignPosX(iconDeadman, GetAllainBasedOnTextKill(textDialog));

            PlaySoundDialogKill(textDialog, characterKiller.GetProfile.GetTeamCharacter);



            timeHide = (float)GameManager.Instance.BattleTime.Elapsed.TotalSeconds + 2.85f;

            StartCoroutine(HideKillDialog());
        }
    }

    public IEnumerator HideKillDialog()
    {
        yield return new WaitForSeconds(3f);

        if (timeHide <= (float)GameManager.Instance.BattleTime.Elapsed.TotalSeconds)
        {
            gameObject.SetActive(false);
        }
    }

    private void PlaySoundDialogKill(string textKill, TeamCharacter teamCharacterWasKiller)
    {
        string pathSound = GetSoundKillPath(textKill, teamCharacterWasKiller);
        audioSource.PlayOneShot(Resources.Load<AudioClip>(pathSound));
    }

    private float GetAllainBasedOnTextKill(string textKill)
    {
        switch (textKill)
        {
            case FristBlood:
                return 92f;
            case DoubleKill:
                return 95f;
            case TripleKill:
                return 89f;
            case QuadraKill:
                return 98.5f;
            case PentaKill:
                return 89f;
            case ShutDown:
                return 97f;
            case Kill:
                return 56f;
            case KillingSpree:
                return 105f;
            case Rampage:
                return 89f;
            case Unstoppable:
                return 105;
            case Dominating:
                return 98.5f;
            case Godlike:
                return 80f;
            case Legendary:
                return 97f;
        }

        return 0;
    }

    private void AlignPosX(Image image, float newPosX)
    {
        image.rectTransform.localPosition = new Vector3(newPosX, image.rectTransform.localPosition.y);
    }

    private string GetSoundKillPath(string textKill, TeamCharacter teamCharacterKiller)
    {
        string soundKillPath = soundPath + "Dialog Kill " + textKill;

        switch (teamCharacterKiller)
        {
            case TeamCharacter.Natural:
            case TeamCharacter.Red:
                soundKillPath += " Red";
                break;
            case TeamCharacter.Blue:
                soundKillPath += " Blue";
                break;
        }

        return soundKillPath;
    }
}
