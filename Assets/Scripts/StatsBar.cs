using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    static readonly string pathHealthBarGreen = "Images/Stats Character/Health Bar Green";
    static readonly string pathHealthBarRed = "Images/Stats Character/Health Bar Red";

    [Header("UI")]
    [SerializeField] Image healthBar;
    [SerializeField] Image divisionLineHealthBar;
    [SerializeField] Image manaBar;

    [SerializeField] Image effectDurationBar;
    [SerializeField] Image effectDurationBackground;

    [SerializeField] Text levelText;
    [SerializeField] Text nameText;

    [SerializeField] Character character;

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

    public static void AddFor(Character character, TypeCharacter typeCharacter, Team team)
    {
        string namePathStastBar = "Prefabs/Stats Bar/Stats Bar " + typeCharacter.ToString();

        StatsBar stastsBar = Instantiate(Resources.Load(namePathStastBar) as GameObject, character.transform).GetComponent<StatsBar>();
        stastsBar.ChangeImageHealthBar(team);
    }


    private void Reset()
    {
        character = GetComponentInParent<Character>();

        if (isUseHealthBar)
        {
            healthBar = transform.Find("Health Bar").GetComponent<Image>();

            if (isUseDivisionLineHealthBar)
            {
                divisionLineHealthBar = transform.Find("Division Line Health Bar").GetComponent<Image>();
            }
        }

        if (isUseManaBar)
        {
            manaBar = transform.Find("Mana Bar").GetComponent<Image>();
        }

        if (isUseEffectBar)
        {
            effectDurationBar = transform.Find("Effect Duration Bar").GetComponent<Image>();
            effectDurationBackground = transform.Find("Effect Duration Bar Background").GetComponent<Image>();
        }

        if (isUseLevelText)
        {
            levelText = transform.Find("Level Text").GetComponent<Text>();
        }

        if (isUseNameText)
        {
            nameText = transform.Find("Name Text").GetComponent<Text>();
        }

        rectTransform = GetComponent<RectTransform>();
    }

    private void Awake()
    {
        Reset();
    }

    private void Start()
    {
        distanceToTarget = this.transform.position - character.transform.position;
        startRotate = transform.rotation;
    }

    void LateUpdate()
    {
        FlowTarget();
        DontRotate();
    }

    private void OnEnable()
    {
        if (isUseHealthBar)
        {
            character.OnHealthChanged += OnHealthChanged;
        }

        if (isUseManaBar)
        {
            character.OnManaChanged += OnManaChanged;
        }

        if (isUseLevelText)
        {
            character.OnLevelChanged += OnLevelChanged;
        }

    }

    private void OnDisable()
    {

        if (isUseHealthBar)
        {
            character.OnHealthChanged -= OnHealthChanged;
        }

        if (isUseManaBar)
        {
            character.OnManaChanged -= OnManaChanged;
        }

        if (isUseLevelText)
        {
            character.OnLevelChanged -= OnLevelChanged;
        }

    }

    void OnHealthChanged(Health health)
    {
        float currentHealthPercentage = health.Current / health.Max;
        healthBar.fillAmount = currentHealthPercentage;
    }

    void OnManaChanged(Mana mana)
    {
        float currentManaPercentage = mana.Current / mana.Max;
        manaBar.fillAmount = currentManaPercentage;
    }

    void OnLevelChanged(Level level)
    {
        levelText.text = level.Current.ToString();
    }

    private void OnTransformParentChanged()
    {
        Debug.Log("Hello!");
    }

    void FlowTarget()
    {
        transform.position = character.transform.position + distanceToTarget;
    }

    void DontRotate()
    {
        transform.rotation = startRotate;
    }

    void ChangeImageHealthBar(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                healthBar.sprite = Resources.Load<Sprite>(pathHealthBarGreen);
                break;
            case Team.Red:
                healthBar.sprite = Resources.Load<Sprite>(pathHealthBarRed);
                break;
            case Team.Natural:
                healthBar.sprite = Resources.Load<Sprite>(pathHealthBarRed);
                break;
            default:
                healthBar.sprite = null;
                break;
        }
    }
}
