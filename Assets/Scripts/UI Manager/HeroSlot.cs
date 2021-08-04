using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    public int number = 0;
    public bool hasCharacter = false;
    private Button btnSelectHero;
    public Character character;
    public static event Action<string> OnHeroNameSelected;
    public static event Action<GameObject,Character> OnHeroSelected;
    private Image icon;
    // Start is called before the first frame update
    void Start()
    {
        btnSelectHero = GetComponent<Button>();
        btnSelectHero.onClick.AddListener(SelectHero);
    }

    //Todo : when choose hero do stuff
    public void SelectHero()
    {
        OnHeroNameSelected?.Invoke(character.information.name);
        OnHeroSelected?.Invoke(gameObject,character);
    }
    public void AddHero(Character character)
    {
        if (true)
        {
            this.character = character;
            icon = GetComponent<Image>();
            icon.sprite = character.information.icon;
        }
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

}
