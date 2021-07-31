using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Setting Load")]
    [SerializeField] string sceneLoadName;
    [SerializeField] float minimumLoadTime;

    [Header("UI Loading")]
    [SerializeField] Image loadingBar;
    [SerializeField] TextMeshProUGUI loadingText;

    Stopwatch loadTimer;


    private void Start()
    {
        StartCoroutine(Load(sceneLoadName, minimumLoadTime));
    }

    IEnumerator Load(string sceneLoadName, float minimumLoadTime)
    {
        loadTimer = new Stopwatch();
        loadTimer.Start();

        float percentLoaded = 0;

        while (loadTimer.Elapsed.TotalSeconds <= 11)
        {
            percentLoaded = (float)(loadTimer.Elapsed.TotalSeconds / minimumLoadTime);

            loadingBar.fillAmount = percentLoaded;
            loadingText.text = string.Format("{0:P0}", percentLoaded);

            yield return null;
        }

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneLoadName);

        while (loadTimer.Elapsed.TotalSeconds <= minimumLoadTime)
        {
            percentLoaded = (float)(loadTimer.Elapsed.TotalSeconds / minimumLoadTime);

            loadingBar.fillAmount = percentLoaded;
            loadingText.text = string.Format("{0:P0}", percentLoaded);

            yield return null;
        }


        loadTimer.Stop();

    }
}
