using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RotateCharacterHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform character;
    Vector2 pointDown;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointDown = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointDrag = eventData.position;
        offset = pointDown - pointDrag;

        RotateCharacter(offset);
        pointDown = pointDrag;
    }

    public void RotateCharacter(Vector2 direction)
    {
        float powerRotate = direction.x / 1.75f;

        character.DOBlendableRotateBy(new Vector2(0, powerRotate), 3f).SetEase(Ease.OutQuart);
    }

    public void SelectCharacter(Transform newCharacter)
    {

    }
}
