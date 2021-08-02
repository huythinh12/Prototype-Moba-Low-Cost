using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    readonly Color ColorGood = new Color(0.537f, 0.843f, 0.012f);
    readonly Color ColorBad = new Color(0.945f, 0.165f, 0.122f);
    readonly float FpsGoodMin = 20f;

    TextMeshProUGUI fpsText;

    // Start is called before the first frame update
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(UpdateText());
    }

    IEnumerator UpdateText()
    {
        float fps = 1f / Time.unscaledDeltaTime;
        fpsText.text = string.Format("FPS {0:N0}", fps);

        if (fps >= FpsGoodMin)
        {
            fpsText.color = ColorGood;
        }
        else
        {
            fpsText.color = ColorBad;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(UpdateText());
    }
}
