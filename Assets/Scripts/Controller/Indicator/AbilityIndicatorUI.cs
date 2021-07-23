using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AbilityIndicatorUI : MonoBehaviour
{
    public static readonly string pathResourceCircleIndicator = "Images/Controller/Circle Indicator";
    public static readonly string pathResourceLinearIndicator = "Images/Controller/Rectangle Indicator";
    public static readonly string pathResourceLineIndicator = "Images/Controller/Line Indicator";

    public RectTransform rangeIndicatorUI;
    public RectTransform shapeIndicatorUI;
    public Image shapeIndicatorImage;

    private float halfWidth;
    Vector3 joystickInput;
    Quaternion startRotate;

    bool isChangePositionWhenHandle;
    bool isChangeRotationWhenHandle;


    public void Start()
    {
        startRotate = transform.rotation;

    }

    public void LateUpdate()
    {
        DontRotate();
    }

    public void ChangeBasedOnIndicator(AbilityIndicatorJoystick abilityIndicatorJoystick)
    {
        joystickInput = new Vector3(abilityIndicatorJoystick.Horizontal, abilityIndicatorJoystick.Vertical);

        if (isChangePositionWhenHandle)
        {
            shapeIndicatorUI.localPosition = joystickInput * halfWidth;
        }

        if (isChangeRotationWhenHandle)
        {
            shapeIndicatorUI.localRotation = Quaternion.Euler(0, -180, JoystickMath.Angle360(abilityIndicatorJoystick.LatePoint));
        }
    }

    public void ShowBaseOnIndicatorType(Character characterFlow, Ability ability)
    {
        transform.SetParent(characterFlow.gameObject.transform, false);
        gameObject.SetActive(true);

        ShowRangeIndicator(ability);
        SetShapeIndicatorHandlingUI(ability);

        if (ability.castRangeMaxCurrent >= 30f)
        {
            float factorView = ability.castRangeMaxCurrent / 28f;

            Camera.main.DOFieldOfView(Camera.main.fieldOfView * factorView, 1f).SetEase(Ease.InOutSine);
        }

    }


    private void SetShapeIndicatorHandlingUI(Ability ability)
    {
        shapeIndicatorUI.localPosition = Vector3.zero;
        shapeIndicatorImage.rectTransform.localPosition = Vector3.zero;
        shapeIndicatorUI.gameObject.SetActive(true);

        switch (ability.indicatorAbilityType)
        {
            case IndicatorAbilityType.Circle:
                {
                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceCircleIndicator);
                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(ability.widthAreaOfEffectCurrent, ability.heightAreaOfEffectCurrent);

                    isChangePositionWhenHandle = true;
                    isChangeRotationWhenHandle = false;
                    break;
                }
            case IndicatorAbilityType.Rectangle:
                {
                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceLinearIndicator);
                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(ability.widthAreaOfEffectCurrent, ability.castRangeMaxCurrent / 2);
                    shapeIndicatorImage.rectTransform.localPosition = new Vector3(0, ability.castRangeMaxCurrent / 4);

                    isChangePositionWhenHandle = false;
                    isChangeRotationWhenHandle = true;
                    break;
                }
            case IndicatorAbilityType.Line:
                {
                    shapeIndicatorImage.sprite = Resources.Load<Sprite>(pathResourceLineIndicator);
                    shapeIndicatorImage.rectTransform.sizeDelta = new Vector2(0.05f, ability.castRangeMaxCurrent / 2);
                    shapeIndicatorImage.rectTransform.localPosition = new Vector3(0, ability.castRangeMaxCurrent / 4);


                    isChangePositionWhenHandle = false;
                    isChangeRotationWhenHandle = true;
                    break;
                }
            default:
                {
                    shapeIndicatorUI.gameObject.SetActive(false);
                    isChangePositionWhenHandle = false;
                    isChangeRotationWhenHandle = false;
                    break;
                }
        }
    }

    private void ShowRangeIndicator(Ability ability)
    {
        rangeIndicatorUI.gameObject.SetActive(true);

        switch (ability.indicatorAbilityType)
        {
            case IndicatorAbilityType.Self:
                rangeIndicatorUI.sizeDelta = new Vector2(1.5f, 1.5f);
                break;
            default:
                rangeIndicatorUI.sizeDelta = new Vector2(ability.castRangeMaxCurrent, ability.castRangeMaxCurrent);
                break;
        }

        halfWidth = rangeIndicatorUI.sizeDelta.x / 2;

    }

    void DontRotate()
    {
        transform.rotation = startRotate;
    }
}
