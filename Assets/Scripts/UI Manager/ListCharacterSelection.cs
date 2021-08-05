using UnityEngine;

public class ListCharacterSelection : MonoBehaviour
{
    public GameObject[] heroes;

    private void OnEnable()
    {
        HeroSlot.OnHeroNameSelected += HandleSelectedHero;
    }
    private void OnDisable()
    {
        HeroSlot.OnHeroNameSelected -= HandleSelectedHero;

    }
    private void HandleSelectedHero(string nameCharacter)
    {
        foreach (Transform item in transform)
        {
            if (item.name.Contains(nameCharacter))
            {
                item.gameObject.SetActive(true);
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
      
    }

   

}
