using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PickHeroHandler : MonoBehaviour
{
    float waitingTime = 5f;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI nameHeroText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateWaitingTime", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateWaitingTime()
    {
        waitingTime--;
        timeText.text = waitingTime.ToString();

        if (waitingTime <= 0f)
        {
            //SceneManager.LoadScene("Battle");
        }
    }

}
