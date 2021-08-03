using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSlot : MonoBehaviour
{
    public int number = 0;
    public bool hasCharacter = false;
    private Button btnSelectHero;
    public Character character;
    private Image icon;
    // Start is called before the first frame update
    void Start()
    {
        icon = GetComponent<Image>();

        //btnSelectHero.onClick.AddListener(SelectHero);
    }

    //Todo : when choose hero do stuff
    public void SelectHero()
    {

    }
    //cái này để test thôi
    public void UpdateUISlot()
    {

        icon.sprite = character.information.icon;
    }
    public void AddHero(Character character)
    {
        if (character)
        {
            this.character = character;

            this.character.information.name = character.information.name;
            icon.sprite = character.information.icon as Sprite;
        }

    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

}
