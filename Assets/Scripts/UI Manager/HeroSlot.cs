using System;
using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.System;

public class HeroSlot : MonoBehaviour
{
    public bool hasCharacter = false;
    private Button btnSelectHero;
    public CharacterSystem characterSystem;
    public static event Action<string> OnHeroNameSelected;
    public static event Action<GameObject, CharacterSystem> OnHeroSelected;
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
        OnHeroNameSelected?.Invoke(characterSystem.GetProfile.Name);
        OnHeroSelected?.Invoke(gameObject, characterSystem);
    }
    public void AddHero(CharacterSystem characterSystem)
    {
        this.characterSystem = characterSystem;
        icon = GetComponent<Image>();
        icon.sprite = characterSystem.GetProfile.IconNormal;
    }

    public void Clear()
    {
        gameObject.SetActive(false);
    }

}
