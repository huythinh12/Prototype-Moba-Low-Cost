using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class BattleDiaglog : MonoBehaviour
{
    [SerializeField] Image iconKiller;
    [SerializeField] Image iconDeadman;
    [SerializeField] TextMeshProUGUI textKill;

    private int sumKill = 0;
    private float timeHide = 0;

    public static string GetTextKill(Character characterKiller, Character characterWasKilled, int sumKill)
    {
        if (sumKill == 1)
        {
            return "First Blood";
        }
        else if (characterKiller.History.AmountHeroKilledContinual >= 2)
        {
            switch (characterKiller.History.AmountHeroKilledContinual)
            {
                case 2:
                    return "Double Kill";
                case 3:
                    return "Triple Kill";
                case 4:
                    return "Quadra Kill";
                default:
                    return "Penta Kill";
            }
        }
        else if (characterWasKilled.History.AmountHeroKilledDiscontinuity >= 3)
        {
            return "Shut down";

        }
        else if (characterKiller.History.AmountHeroKilledDiscontinuity <= 2)
        {
            return "Kill";
        }
        else if (characterKiller.History.AmountHeroKilledDiscontinuity >= 2)
        {
            switch (characterKiller.History.AmountHeroKilledDiscontinuity)
            {
                case 3:
                    return "Killing spree";
                case 4:
                    return "Rampage";
                case 5:
                    return "Unstoppable";
                case 6:
                    return "Dominating";
                case 7:
                    return "Godlike";
                default:
                    return "Legendary";
            }
        }
        else
        {
            return "ERROR!";
        }
    }


    public void ShowKillDialog(Character characterWasKilled)
    {
        if (characterWasKilled.information.typeCharacter == TypeCharacter.Hero)
        {
            gameObject.SetActive(true);


            Character characterKiller = characterWasKilled.History.GetCharacterKill();

            iconKiller.sprite = characterKiller.information.icon;
            iconDeadman.sprite = characterWasKilled.information.icon;

            sumKill++;

            textKill.text = BattleDiaglog.GetTextKill(characterKiller, characterWasKilled, sumKill);



            timeHide = (float)GameManager.Instance.BattleTime.Elapsed.TotalSeconds + 2.99f;
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


}
