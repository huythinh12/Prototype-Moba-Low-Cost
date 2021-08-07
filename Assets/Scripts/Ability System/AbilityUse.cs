//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Diagnostics;
//using DG.Tweening;
//using CharacterMechanism.System;

//[DefaultExecutionOrder(1000)]
//public class AbilityUse : MonoBehaviour
//{
//    public static readonly Color ColorWhenDisabled = new Color(0.58f, 0.58f, 0.58f);
//    public static readonly Color ColorWhenIndicatorHandle = new Color(0.3f, 0.3f, 0.3f);
//    public static readonly Color ColorWhenCanNotUse = new Color(0.18f, 0.18f, 0.18f);

//    public static readonly float ScaleFactorWhenIndicatorHandle = 0.85f;

//    public Ability ability;
//    public CharacterSystem self;

//    public AbilityIndicatorJoystick abilityIndicatorJoystick;
//    public AbilityIndicatorUI abilityIndicatorUI;

//    public Image skillImage;
//    public Image cooldownImage;
//    public Image backgroundSkillImage;
//    public Image levelBackgroundSkillImage;
//    public Image levelDivisionSkillImage;

//    public RectTransform SkillUI;

//    public ClassifyAbility classifyAbility;


//    public void Reset()
//    {
//        self = GameObject.FindObjectOfType<PlayerController>().GetComponent<Character>();
//        SkillUI = transform.Find("Skill UI").GetComponent<RectTransform>() as RectTransform;
//        skillImage = SkillUI.transform.Find("Skill Image").GetComponent<Image>();
//        cooldownImage = SkillUI.transform.Find("Cooldown Image").GetComponent<Image>();
//        backgroundSkillImage = SkillUI.transform.Find("Background Skill Image").GetComponent<Image>();
//        levelBackgroundSkillImage = SkillUI.transform.Find("Level Background Skill Image").GetComponent<Image>();
//        levelDivisionSkillImage = SkillUI.transform.Find("Level Division Skill Image").GetComponent<Image>();
//        abilityIndicatorUI = GameObject.FindObjectOfType<AbilityIndicatorUI>();
//        abilityIndicatorJoystick = transform.Find("Ability Indicator Joystick").GetComponent<AbilityIndicatorJoystick>();
//    }


//    public void Awake()
//    {
//        Reset();
//    }

//    public void OnEnable()
//    {
//        abilityIndicatorJoystick.OnIndicationStart += ShowIndicatorUI;
//        abilityIndicatorJoystick.OnIndicationDrag += abilityIndicatorUI.ChangeBasedOnIndicator;
//        abilityIndicatorJoystick.OnIndicationDone += UseAbility;

//        self.OnManaChanged += UpdateCanUse;
//    }

//    public void Start()
//    {
//        cooldownImage.gameObject.SetActive(false);
//        abilityIndicatorUI.gameObject.SetActive(false);
//        //abilityIndicatorUI.gameObject.SetActive(false);

//        switch (classifyAbility)
//        {
//            case ClassifyAbility.Unset:
//                break;
//            case ClassifyAbility.Alpha:
//                ability = AbilityDatabase.Instance.GetAbility(self.information.AlphaAbilityName);
//                break;
//            case ClassifyAbility.Beta:
//                ability = AbilityDatabase.Instance.GetAbility(self.information.BetaAbilityName);
//                break;
//            case ClassifyAbility.Ultimate:
//                ability = AbilityDatabase.Instance.GetAbility(self.information.UltimateAbilityName);
//                break;
//            case ClassifyAbility.Recall:
//                break;
//            default:
//                break;
//        }

//        skillImage.sprite = ability.abilityData.Icon;
//    }

//    public void UpdateCanUse(CharacterStats stats)
//    {
//        if (stats.ManaCurrent < ability.abilityData.ManaCost)
//        {
//            abilityIndicatorJoystick.gameObject.SetActive(false);
//            skillImage.color = ColorWhenCanNotUse;
//        }
//        else
//        {
//            abilityIndicatorJoystick.gameObject.SetActive(true);
//            skillImage.color = Color.white;
//        }
//    }

//    public void UseAbility(Vector3 indicatorXY)
//    {
//        if (ability.abilityData.RangeCast.Value >= 30)
//        {
//            Camera.main.DOFieldOfView(40, 1.85f).SetEase(Ease.InOutSine);
//        }

//        abilityIndicatorJoystick.gameObject.SetActive(false);
//        SetScaleImage(false);
//        Vector3 indicatorXZ = JoystickMath.ConvertToOxzIndicator(indicatorXY);
//        abilityIndicatorUI.gameObject.SetActive(false);



//        self.StartCoroutine(ability.Excecute(self, indicatorXZ));


//        //if (ability.UseAblity(self, indicatorXZ))
//        //{
//        StartCoroutine(StartCooldown());
//        //}
//    }

//    public IEnumerator StartCooldown()
//    {

//        skillImage.color = ColorWhenDisabled;
//        cooldownImage.gameObject.SetActive(true);

//        Stopwatch cooldownTimer = new Stopwatch();
//        cooldownTimer.Start();

//        while (cooldownTimer.Elapsed.TotalSeconds < ability.abilityData.Cooldown)
//        {
//            cooldownImage.fillAmount = (float)(1 - cooldownTimer.Elapsed.TotalSeconds / ability.abilityData.Cooldown);
//            yield return false;
//        }

//        abilityIndicatorJoystick.gameObject.SetActive(true);

//        skillImage.color = Color.white;
//        cooldownImage.gameObject.SetActive(false);

//        UpdateCanUse(self.Stats);
//    }

//    public void ShowIndicatorUI(int n)
//    {
//        skillImage.color = ColorWhenIndicatorHandle;
//        SetScaleImage(true);

//        abilityIndicatorUI.ShowBaseOnIndicatorType(self, ability);
//    }

//    public void SetScaleImage(bool isIndicatorHandle)
//    {
//        float scaleDuration = 0.3f;
//        Ease easeScale = Ease.InOutQuint;
//        float scaleFactor = isIndicatorHandle ? ScaleFactorWhenIndicatorHandle : 1;

//        SkillUI.DOScale(scaleFactor, scaleDuration).SetEase(easeScale);
//    }
//}
