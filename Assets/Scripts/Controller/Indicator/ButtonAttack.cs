using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAttack : MonoBehaviour, IPointerDownHandler
{
    Character character;
    AbilityIndicatorUI abilityIndicatorUI;

    public void Awake()
    {
        character = FindObjectOfType<PlayerController>().GetComponent<Character>();
        abilityIndicatorUI = GameObject.FindObjectOfType<AbilityIndicatorUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        abilityIndicatorUI.ShowRangeAttack(character);
        character.Attack();
    }
}
