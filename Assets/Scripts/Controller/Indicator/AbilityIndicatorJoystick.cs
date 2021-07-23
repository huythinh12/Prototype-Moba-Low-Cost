using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AbilityIndicatorJoystick : Joystick
{
    public int scaleWhenDrag = 94;
    public int scaleWhenDone = 45;

    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }
    public Vector3 LatePoint;

    [SerializeField] private float moveThreshold = 1;
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;

    private Vector2 fixedPosition = Vector2.zero;
    public event Action<int> OnIndicationStart;
    public event Action<AbilityIndicatorJoystick> OnIndicationDrag;
    public event Action<Vector3> OnIndicationDone;

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

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;
        SetMode(joystickType);

        SetShowImage(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);
        OnIndicationStart?.Invoke(0);

        background.sizeDelta = new Vector2(scaleWhenDrag, scaleWhenDrag);

        SetShowImage(true);

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (joystickType != JoystickType.Fixed)
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);
        OnIndicationDone?.Invoke(LatePoint);

        background.sizeDelta = new Vector2(scaleWhenDone, scaleWhenDone);
        SetShowImage(false);

    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        LatePoint = Direction;

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


    void SetShowImage(bool haveColor)
    {
        if (haveColor)
        {
            background.GetComponent<Image>().color = Color.white;
            handle.GetComponent<Image>().color = Color.white;
        }
        else
        {
            background.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            handle.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }
}
