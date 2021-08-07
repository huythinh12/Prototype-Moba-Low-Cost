using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(Canvas),typeof(CanvasScaler), typeof(GraphicRaycaster))]
public class FloatDamgeText : MonoBehaviour
{
    static readonly Vector3 DirectionFloat = new Vector3(0, 0.17f, 0);
    static readonly float DurationMove = 1.5f;
    static readonly WaitForSeconds TimeLife = new WaitForSeconds(1.5f);
    static readonly string pathResource = "Prefabs/Float Text/Visual Dame";

    [SerializeField]
    private Image criticalIcon;
    [SerializeField]
    private TextMeshProUGUI damgeText;

    public static FloatDamgeText Create(Vector3 position, float amountDamge, DamageType damgeType, bool isCritical = false)
    {
        FloatDamgeText floatDamgeText = Instantiate(Resources.Load<FloatDamgeText>(pathResource));
       

        return floatDamgeText;






        //GameObject visualDameGameobject = new GameObject("Visual Dame Text ");
        //FloatDamgeText floatDamgeText;

        //floatDamgeText = new GameObject("Visual Dame Text", typeof(FloatDamgeText), typeof(RectTransform)).GetComponent<FloatDamgeText>();
        //floatDamgeText.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        //floatDamgeText.transform.SetParent(visualDameGameobject.transform);
        //floatDamgeText.GetComponent<Transform>().localPosition = Vector3.zero;
        //RectTransform floatDamgeTextRectTransform = floatDamgeText.GetComponent<RectTransform>();
        //visualDameGameobject.transform.position = new Vector3(10,0,5);
    }


    private void OnEnable()
    {
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        transform.DOMove(transform.position + DirectionFloat, DurationMove);
        yield return TimeLife;
        //Destroy(gameObject);
    }
   
}
