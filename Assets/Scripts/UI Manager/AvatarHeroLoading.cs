using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class AvatarHeroLoading : MonoBehaviour
{
    public TMP_Text heroName;
    public TMP_Text textLoading;
    public Image loadingBar;
    public static int countReady = 0;
    Stopwatch loadTimer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load(Random.Range(6, 12)));

    }
    IEnumerator Load(float minimumLoadTime)
    {
        loadTimer = new Stopwatch();
        loadTimer.Start();

        float percentLoaded = 0;

        while (loadTimer.Elapsed.TotalSeconds <= minimumLoadTime)
        {
            percentLoaded = (float)(loadTimer.Elapsed.TotalSeconds / minimumLoadTime);

            loadingBar.fillAmount = percentLoaded;
            textLoading.text = string.Format("{0:P0}", percentLoaded);

            yield return null;
        }

        loadTimer.Stop();

        //condition to load scene battle
        countReady++;
        if (countReady == 6)
            SceneManager.LoadScene("Battle");
    }
}
