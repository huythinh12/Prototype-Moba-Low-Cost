using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[DefaultExecutionOrder(1000)]
public class KDAUI : MonoBehaviour
{
    Character player;

    [SerializeField] TextMeshProUGUI killText;
    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] TextMeshProUGUI assistText;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().GetComponent<Character>();
        GameManager.Instance.kdaPlayerUI = this;
    }

    public void UpdateKDAText()
    {
        killText.text = player.History.Kill.ToString();
        deathText.text = player.History.Death.ToString();
        assistText.text = player.History.Assist.ToString();
    }
}
