//using UnityEngine;
//using UnityEngine.UI;
//using DG.Tweening;

//public class AbilityIndicatorUI : MonoBehaviour
//{
//    public static readonly string pathResourceCircleIndicator = "Images/Controller/Circle Indicator";
//    public static readonly string pathResourceLinearIndicator = "Images/Controller/Rectangle Indicator";
//    public static readonly string pathResourceLineIndicator = "Images/Controller/Line Indicator";

//    public RectTransform rangeIndicatorUI;
//    public RectTransform shapeIndicatorUI;
//    public Image shapeIndicatorImage;

//    private float halfWidth;
//    Vector3 joystickInput;
//    Quaternion startRotate;

//    bool isChangePositionWhenHandle;
//    bool isChangeRotationWhenHandle;


//    public void Start()
//    {
//        startRotate = transform.rotation;
//    }

//    public void LateUpdate()
//    {
//        DontRotate();
//    }

//    public void ChangeBasedOnIndicator(AbilityIndicatorJoystick abilityIndicatorJoystick)
//    {
//        joystickInput = new Vector3(abilityIndicatorJoystick.Horizontal, abilityIndicatorJoystick.Vertical);

//        if (isChangePositionWhenHandle)
//        {
//            shapeIndicatorUI.localPosition = joystickInput * halfWidth;
//        }

//        if (isChangeRotationWhenHandle)
//        {
//            shapeIndicatorUI.localRotation = Quaternion.Euler(0, -180, JoystickMath.Angle360(abilityIndicatorJoystick.LatePoint));
//        }
//    }

//    public void ShowBaseOnIndicatorType(Character characterFlow, Ability ability)
//    {
//        transform.SetParent(characterFlow.gameObject.transform, false);
//        gameObject.SetActive(true);

//        ShowRangeIndicator(ability);
//        SetShapeIndicatorHandlingUI(ability);

//        if (ability.abilityData.RangeCast.Value >= 30f)
//        {
//            float factorView = ability.abilityData.RangeCast.Value / 28f;

//            Camera.main.DOFieldOfView(Camera.main.fieldOfView * factorView, 1f).SetEase(Ease.InOutSine);
//        }
//    }

//    public void ShowRangeAttack(Character character)
//    {
//        transform.SetParent(character.gameObject.transform, false);
//        gameObject.SetActive(true);

//        shapeIndicatorUI.gameObject.SetActive(false);
//        rangeIndicatorUI.gameObject.SetActive(true);
//        rangeIndicatorUI.sizeDelta = new Vector2(character.Stats.RangeAttack.Value * 2, character.Stats.RangeAttack.Value * 2);

//    }


//    private void SetShapeIndicatorHandlingUI(Ability ability)
//    {
//        shapeIndicatorUI.localPosition = Vector3.zero;

//        shapeIndicatorUI.rotation = new Quaternion(0, 0, 0, 0);
//        shapeIndicatorImage.rectTransform.localPosition = Vector3.zero;
//        shapeIndicatorUI.gameObject.SetActive(true);

//        switch (ability.abilityData.indicatorAbilityType)
//        {
//            case IndicatorAbilityType.Circle:
//                {
//                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceCircleIndicator);
//                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(ability.abilityData.Radius.Value, ability.abilityData.Radius.Value);

//                    isChangePositionWhenHandle = true;
//                    isChangeRotationWhenHandle = false;
//                    break;
//                }
//            case IndicatorAbilityType.Rectangle:
//                {
//                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceLinearIndicator);
//                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(ability.abilityData.WidthAreaOfEffect.Value, ability.abilityData.RangeCast.Value / 2);
//                    shapeIndicatorImage.rectTransform.localPosition = new Vector3(0, ability.abilityData.RangeCast.Value / 4);

//                    isChangePositionWhenHandle = false;
//                    isChangeRotationWhenHandle = true;
//                    break;
//                }
//            case IndicatorAbilityType.Line:
//                {
//                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceLineIndicator);
//                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(0.05f, ability.abilityData.RangeCast.Value / 2);
//                    shapeIndicatorImage.rectTransform.localPosition = new Vector3(0, ability.abilityData.RangeCast.Value / 4);


//                    isChangePositionWhenHandle = false;
//                    isChangeRotationWhenHandle = true;
//                    break;
//                }
//            default:
//                {
//                    shapeIndicatorUI.gameObject.SetActive(false);
//                    isChangePositionWhenHandle = false;
//                    isChangeRotationWhenHandle = false;
//                    break;
//                }
//        }
//    }

//    private void ShowRangeIndicator(Ability ability)
//    {
//        rangeIndicatorUI.gameObject.SetActive(true);

//        switch (ability.abilityData.indicatorAbilityType)
//        {
//            case IndicatorAbilityType.Self:
//                rangeIndicatorUI.sizeDelta = new Vector2(1.5f, 1.5f);
//                break;
//            default:
//                rangeIndicatorUI.sizeDelta = new Vector2(ability.abilityData.RangeCast.Value, ability.abilityData.RangeCast.Value);
//                break;
//        }

//        halfWidth = rangeIndicatorUI.sizeDelta.x / 2;

//    }

//    void DontRotate()
//    {
//        transform.rotation = Quaternion.Euler(90, 0, 0);
//    }
//}
