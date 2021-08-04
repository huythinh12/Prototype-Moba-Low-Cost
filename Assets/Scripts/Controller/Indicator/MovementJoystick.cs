using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using CharacterMechanism.System;
using DG.Tweening;

[DefaultExecutionOrder(1000)]
public class MovementJoystick : Joystick
{
    static string joystickPrefabPath = "Prefabs/Controller/Movement Joystick";

    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }
    private Vector3 latePoint;

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;
    public event Action<MovementJoystick> OnIndicationDrag;
    public event Action<MovementJoystick> OnIndicationDone;

    static public MovementJoystick Create(CharacterSystem characterSystem, Canvas canvasParent)
    {
        MovementJoystick movementJoystick = Instantiate(Resources.Load<MovementJoystick>(joystickPrefabPath), canvasParent.transform);
        return movementJoystick;
    }


    public void SetMode(JoystickType joystickType)
    {
        this.joystickType = joystickType;
        if (joystickType == JoystickType.Fixed)
        {
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
            background.gameObject.SetActive(false);
    }

    public Vector3 GetDirectionXZ => new Vector3(latePoint.x, 0, latePoint.y).normalized;

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);

        ShowStyleFade(1.5f);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
        OnIndicationDone?.Invoke(this);

        latePoint = Vector3.zero;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        latePoint = Direction;

        OnIndicationDrag?.Invoke(this);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }

    void ShowStyleFade(float duration)
    {
        Image backgroundImage = background.GetComponent<Image>();
        Image handlerImage = handle.GetComponent<Image>();

        float backgroundImageAlphaStart = backgroundImage.color.a;
        float handlerImageAlphaStart = handlerImage.color.a;
        float factorAlpha = handlerImageAlphaStart / backgroundImageAlphaStart;

        //Set color's alpha to transparent
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.b, backgroundImage.color.g, 0);
        handlerImage.color = new Color(handlerImage.color.r, handlerImage.color.b, handlerImage.color.g, 0);

        backgroundImage.DOFade(backgroundImageAlphaStart, duration * factorAlpha).SetEase(Ease.InQuint);
        handlerImage.DOFade(backgroundImageAlphaStart, duration).SetEase(Ease.InQuint);
    }
}
