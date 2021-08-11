using UnityEngine;
using UnityEngine.UI;
using CharacterMechanism.System;

public sealed class StatsBar : MonoBehaviour
{
    ////////////////////////////////
    ////////// Attribute ///////////
    ////////////////////////////////

    /////////////////////////////////
    ///////// Resource Paths ////////

    static readonly string PathStatBar = "Prefabs/Stats Bar/Stats Bar ";
    static readonly string PathHealthBarGreen = "Images/Stats Character/Health Bar Green";
    static readonly string PathHealthBarRed = "Images/Stats Character/Health Bar Red";

    //////////////////////////////////
    ///////// Name Convention ////////

    static readonly string NameHealthBarChild = "Health Bar";
    static readonly string NameManaBarChild = "Mana Bar";
    static readonly string NameDivisionLineHealthBarChild = "Division Line Health Bar";
    static readonly string NameLevelTextChild = "Level Text";
    static readonly string NameNameTextChild = "Name Text";


    /////////////////////////////////
    //// Character System Parent ////

    [SerializeField] CharacterSystem characterSystem;

    //////////////////////////////
    //////////// UI //////////////

    [Header("UI")]
    [SerializeField] Image healthBar;
    [SerializeField] Image divisionLineHealthBar;
    [SerializeField] Image manaBar;
    [SerializeField] Text levelText;
    [SerializeField] Text nameText;

    ///////////////////////////////
    /////// Setting Use ///////////

    [Header("Setting Use")]
    [SerializeField] bool isUseHealthBar = true;
    [SerializeField] bool isUseDivisionLineHealthBar = false;

    [SerializeField] bool isUseManaBar = false;
    [SerializeField] bool isUseEffectBar = false;
    [SerializeField] bool isUseLevelText = false;
    [SerializeField] bool isUseNameText = false;

    RectTransform rectTransform;
    Vector3 distanceToTarget;
    Quaternion startRotate;

    ////////////////////////////
    ////////// Method //////////
    ////////////////////////////

    //////////////////////////////
    //////////// API /////////////

    /// <summary>
    /// Create a Stat Bar for a Character System object based on Type and Team Character 
    /// </summary>
    static public StatsBar Create(CharacterSystem characterSystem)
    {
        string namePathStastBar = PathStatBar + characterSystem.GetProfile.GetTypeCharacter;

        StatsBar stastsBar = Instantiate(Resources.Load<StatsBar>(namePathStastBar), characterSystem.transform);
        stastsBar.characterSystem = characterSystem;
        stastsBar.EventRegistrationFormCharacterSystem(characterSystem);

        return stastsBar;
    }

    private void EventRegistrationFormCharacterSystem(CharacterSystem characterSystem)
    {
        if (isUseLevelText)
            characterSystem.OnLevelChange += HandleLevelChangel;

        if (isUseHealthBar)
            characterSystem.OnHealthChange += HandleHealthChange;

        if (isUseManaBar)
            characterSystem.OnManaChange += HandleManaChange;
    }

    //////////////////////////////
    ////////// Callback //////////

    private void LoadComponents()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void SetChildObjects()
    {
        healthBar = isUseHealthBar ? transform.Find(NameHealthBarChild).GetComponent<Image>() : null;
        divisionLineHealthBar = isUseDivisionLineHealthBar ? transform.Find(NameDivisionLineHealthBarChild).GetComponent<Image>() : null;
        manaBar = isUseManaBar ? transform.Find(NameManaBarChild).GetComponent<Image>() : null;
        levelText = isUseLevelText ? transform.Find(NameLevelTextChild).GetComponent<Text>() : null;
        nameText = isUseLevelText ? transform.Find(NameNameTextChild).GetComponent<Text>() : null;
    }

    ///////////////////////////////////////
    //////////// Handle Event /////////////

    private void HandleLevelChangel(CharacterSystem characterSystem)
    {
        levelText.text = characterSystem.GetProfile.Level.ToString();
    }

    private void HandleHealthChange(CharacterSystem characterSystem)
    {
        float currentPercentage = characterSystem.GetProfile.HealthCurrent / characterSystem.GetProfile.HealthMax.Value;
        healthBar.fillAmount = currentPercentage;
    }

    private void HandleManaChange(CharacterSystem characterSystem)
    {
        float currentPercentage = characterSystem.GetProfile.ManaCurrent / characterSystem.GetProfile.ManaMax.Value;
        healthBar.fillAmount = currentPercentage;
    }

    ///////////////////////////////////////
    //////////// Handle Image /////////////

    void ChangeImageHealthBar(TeamCharacter teamCharacter)
    {
        switch (teamCharacter)
        {
            case TeamCharacter.Blue:
                healthBar.sprite = Resources.Load<Sprite>(PathHealthBarGreen);
                break;
            case TeamCharacter.Red:
            case TeamCharacter.Natural:
                healthBar.sprite = Resources.Load<Sprite>(PathHealthBarRed);
                break;
        }
    }

    ///////////////////////////////////////
    //////////// Flow Target //////////////

    private void FlowTarget()
    {
        transform.position = characterSystem.transform.position + distanceToTarget;
    }

    private void DontRotate()
    {
        transform.rotation = startRotate;
    }

    ////////////////////////////////////////////
    ////////// MonoBehaviour Callback //////////

    private void Awake()
    {
        SetChildObjects();
    }

    private void Start()
    {
        ChangeImageHealthBar(characterSystem.GetProfile.GetTeamCharacter);
        distanceToTarget = this.transform.position - characterSystem.transform.position;
        startRotate = transform.rotation;
    }

    private void LateUpdate()
    {

        FlowTarget();
        DontRotate();
    }
}
