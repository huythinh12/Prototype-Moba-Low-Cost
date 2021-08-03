using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInventory : MonoBehaviour
{
    HeroSlot[] heroSlots;
    private List<Character> listCharacter = new List<Character>();

    //private List<GameObject> heroSlot = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        heroSlots = transform.GetComponentsInChildren<HeroSlot>();

        //cái này để test thôi
        if (CharacterDatabase.Instance.Database.Count > 0)
        {
            print(CharacterDatabase.Instance.Database.Count + "co database ");
        }
        AddCharacterToListUI();

    }

    private void AddCharacterToListUI()
    {
        var index = 0;
        foreach (var character in CharacterDatabase.Instance.Database)
        {
            if (index < CharacterDatabase.Instance.Database.Count)
            {
                if (character.Value.information.icon)
                {
                    heroSlots[index].AddHero(character.Value);
                    index++;
                }
            }

        }
        foreach (var hero in heroSlots)
        {
            if (hero.hasCharacter == false)
                hero.gameObject.SetActive(false);
            else
            {
                hero.UpdateUISlot();
            }
        }

    }

    //private void AddHeroIcon(int listCount)
    //{

    //    var index = 0;
    //    foreach (Transform hero in transform)
    //    {
    //        if (heroSlot.Count < listCount)
    //        {
    //            heroSlot.Add(hero.gameObject);
    //            hero.gameObject.SetActive(true);
    //            hero.gameObject.GetComponent<Image>().sprite = listCharacter[index].information.icon;
    //            index++;
    //        }
    //    }
    //}

    
}
