//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using DG.Tweening;

//[DefaultExecutionOrder(200), RequireComponent(typeof(Rigidbody), typeof(AudioSource), typeof(BoxCollider))]
//public class Character : MonoBehaviour
//{
//    public static readonly Vector3 revivalPositionBlue = new Vector3(5.7f, 0, -8.2f); // Vector3(-70f, 0f, 4f)
//    public static readonly Vector3 revivalPositionRed = new Vector3(5.7f, 0, -8.2f); //new Vector3(70f, 0f, 4f); 
//    public static readonly float AttackDetectionRange = 5f;

//    public string NameID;
//    public TeamCharacter team;

//    //public CharacterInformation information;
//    //public CharacterStats Stats;
//    //public CharacterHistory History;
//    [HideInInspector] public CharacterCombatController CombatController;
//    [HideInInspector] public CharacterMovementController MovementController;
//    [HideInInspector] public CharacterSound sound;

//    [Space()]
//    public CharacterAttachPoint AttachPoint;

//    [HideInInspector] public new Rigidbody rigidbody;
//    [HideInInspector] public AudioSource audioSource;
//    private new Collider collider;
//    [HideInInspector] public Animator animator;
//    private AnimationClip[] animationClips;
//    //private StatsBar statsBar;

//    //public bool isAlive = true;
//    //public bool isCanMove = true;

//    //public event Action<int> OnLevelChanged;
//    //public event Action<CharacterStats> OnHealthChanged;
//    //public event Action<CharacterStats> OnManaChanged;
//    //public event Action<float> OnTakeDamage;
//    //public event Action<float> OnHealingHealth;
//    //public event Action<Character> OnDie;

//    //private AIHeroes aiHeroes;


//    //public Character(CharacterInformation characterInformation, CharacterStats characterStats, CharacterSound characterSound)
//    //{

//    //    this.information = new CharacterInformation();
//    //    this.information = characterInformation;

//    //    this.Stats = new CharacterStats();
//    //    this.Stats = characterStats;

//    //    this.sound = characterSound;

//    //}


//    public static bool IsAlly(Character characterA, Character characterB)
//    {
//        return characterA.team == characterB.team;
//    }

//    public static bool IsTarget(Character characterA, Character characterB)
//    {
//        return !(IsAlly(characterA, characterB));
//    }

//    void Start()
//    {
//        LoadDataFromDataBase();

//        History = new CharacterHistory(this);
//        CombatController = new CharacterCombatController(this);
//        MovementController = new CharacterMovementController(this);

//        GetComponentsDefault();
//        AddStatsBar();
//        SetStatsDefault();
//        RegistrateEvents();

//        SetLayer("Character");


//        OnDie += GameManager.Instance.battleDiaglog.ShowKillDialog;

//        isAlive = true;
//        isCanMove = true;

//        StartCoroutine(PlaySoundMovement());
//    }

//    private void Update()
//    {

//    }


//    //void LoadDataFromDataBase()
//    //{
//    //    Character rawCharacter = CharacterDatabase.Instance.GetCharacter(NameID);
//    //    //Character rawCharacterClone = new Character(rawCharacter.information, rawCharacter.Stats, rawCharacter.sound);

//    //    //this.information = new CharacterInformation();
//    //    //this.Stats = new CharacterStats();
//    //    //this.sound = new CharacterSound();
//    //    //new Character(rawCharacterClone.information, rawCharacterClone.Stats, rawCharacterClone.sound);

//    //    this.information = new CharacterInformation();
//    //    information = rawCharacter.information.Clone() as CharacterInformation;

//    //    this.Stats = new CharacterStats();
//    //    Stats = rawCharacter.Stats;

//    //    sound = rawCharacter.sound;

//    //    this.information.name += UnityEngine.Random.Range(1000, 9999).ToString();

//    //}

//    //void SetLayer(string layer)
//    //{
//    //    gameObject.layer = LayerMask.NameToLayer(layer);
//    //}


//    //void GetComponentsDefault()
//    //{
//    //    //aiHeroes = GetComponent<AIHeroes>();

//    //    animator = GetComponent<Animator>();
//    //    rigidbody = GetComponent<Rigidbody>();
//    //    collider = GetComponent<Collider>();
//    //    audioSource = GetComponent<AudioSource>();

//    //    if (information.typeCharacter != TypeCharacter.Tower)
//    //    {
//    //        animationClips = animator.runtimeAnimatorController.animationClips;
//    //    }
//    //}

//    //public void AddStatsBar()
//    //{
//    //    statsBar = StatsBar.AddFor(this, information.typeCharacter, team);
//    //}

//    //public void SetStatsDefault()
//    //{
//    //    //Stats.SubscribeOnLevelChange(this);

//    //    Stats.ResetHealthCurrent();
//    //    Stats.ResetManaCurrent();

//    //    HandleEventGeneralStatsChanged();
//    //}

//    //public void RegistrateEvents()
//    //{
//    //    OnDie += GameManager.Instance.UpdateScoreTexts;
//    //}

//    //private Vector3 GetRevivalPosition()
//    //{
//    //    switch (team)
//    //    {
//    //        case TeamCharacter.Natural:
//    //            return Vector3.zero;
//    //        case TeamCharacter.Blue:
//    //            return revivalPositionBlue;
//    //        case TeamCharacter.Red:
//    //            return revivalPositionRed;
//    //        default:
//    //            return Vector3.zero;
//    //    }
//    //}
//    //End



//    //Ability behavior




//    //public Character FindTargetInDetectionRange()
//    //{
//    //    Collider[] colliders = Physics.OverlapSphere(transform.position, AttackDetectionRange);

//    //    foreach (var collider in colliders)
//    //    {
//    //        if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
//    //        {
//    //            return collider.GetComponent<Character>();
//    //        }
//    //    }

//    //    return null;
//    //}

//    //public Character FindTargetInRangeAttack()
//    //{
//    //    Collider[] colliders = Physics.OverlapSphere(transform.position, Stats.RangeAttack.Value);

//    //    foreach (var collider in colliders)
//    //    {
//    //        if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
//    //        {
//    //            return collider.GetComponent<Character>();
//    //        }
//    //    }

//    //    return null;
//    //}


//    //public void Attack()
//    //{
//    //    if (isAlive)
//    //    {
//    //        MovementController.StopMove();
//    //        StartCoroutine(PlayAnimationOneShot(AnimationCharacterType.Attack));

//    //        Collider[] colliders = Physics.OverlapSphere(transform.position, Stats.RangeAttack.Value);


//    //        foreach (var collider in colliders)
//    //        {

//    //            if (collider.GetComponent<Character>() != null)
//    //            {
//    //                if (collider.GetComponent<Character>().team != this.team)
//    //                {
//    //                    collider.GetComponent<Character>().CombatController.TakeDamage(this, DamageType.Physical, Stats.PhysicalDamage.Value);

//    //                    transform.DOLookAt(collider.transform.position, 0f);
//    //                }
//    //            }
//    //        }
//    //    }
//    //}

//    //public void Glide(Vector3 direction, float duration, Ease easeGlide = Ease.InOutCubic)
//    //{
//    //    if (isAlive)
//    //    {
//    //        transform.DOLookAt(transform.position + direction, 0f);
//    //        rigidbody.DOMove(transform.position + direction, duration).SetEase(easeGlide);
//    //    }
//    //}

//    //public void JumpTo(Vector3 direction, float duration)
//    //{
//    //    MovementController.StopMove();

//    //    transform.DOLookAt(transform.position + direction, 0f);
//    //    transform.DOJump(transform.position + direction, 10f, 1, duration).SetEase(Ease.InOutExpo);

//    //    MovementController.StopMove();
//    //}

//    //public IEnumerator Revival()
//    //{
//    //    yield return new WaitForSeconds(GetTimeRevialHero());

//    //    transform.position = GetRevivalPosition();
//    //    isAlive = true;

//    //    statsBar.gameObject.SetActive(true);
//    //    collider.enabled = true;

//    //    Stats.ResetHealthCurrent();
//    //    Stats.ResetManaCurrent();
//    //    HandleEventGeneralStatsChanged();

//    //    animator.SetBool("isAlive", isAlive);

//    //    History.Reset();

//    //    //if (aiHeroes)
//    //    //{
//    //    //    aiHeroes.enabled = true;
//    //    //}
//    //}

//    //public void CheckDie()
//    //{
//    //    if (Stats.HealthCurrent <= 0)
//    //    {
//    //        isAlive = false;

//    //        statsBar.gameObject.SetActive(false);
//    //        collider.enabled = false;

//    //        animator.SetTrigger("Die");
//    //        animator.SetBool("isAlive", isAlive);
//    //        //ToDo:
//    //        //coroutine
//    //        //StartCoroutine(Revival());
//    //        //gameObject.SetActive(false);
//    //        Debug.Log(string.Format("{0} will revial after {1} seconds!", name, GetTimeRevialHero()));
//    //        StartCoroutine(Revival());

//    //        HandleEventDie();

//    //        //if (aiHeroes)
//    //        //{
//    //        //    aiHeroes.enabled = false;
//    //        //}

//    //    }
//    //}
//    //End

//    //public object Clone()
//    //{
//    //    //var character = new Character(this.information, this.Stats, this.sound);

//    //    //throw new NotImplementedException();
//    //    var character = new Character(this.information, this.Stats, this.sound)
//    //    {
//    //        //            information = new CharacterInformation();
//    //        //Stats = new CharacterStats();
//    //        //sound = new CharacterSound();

//    //        //character.information = this.information;
//    //    };



//    //    return character;
//    //}


//    //#region Events Handle
//    //public void HandleEventGeneralStatsChanged()
//    //{
//    //    HandleEventHealthChanged();
//    //    HandleEventManaChanged();
//    //    HandleEventLevelChanged();
//    //}

//    ///// <summary>
//    ///// Should use "HandleEventHealingHealth()" or "HandleEventTakeDamage()" instead
//    ///// </summary>
//    //public void HandleEventHealthChanged()
//    //{
//    //    OnHealthChanged?.Invoke(Stats);
//    //    CheckDie();
//    //}

//    //public void HandleEventManaChanged()
//    //{
//    //    OnManaChanged?.Invoke(Stats);
//    //}

//    //public void HandleEventLevelChanged()
//    //{
//    //    OnLevelChanged?.Invoke(Stats.Level);
//    //}

//    //public void HandleEventHealingHealth(float amount)
//    //{
//    //    OnHealingHealth?.Invoke(amount);
//    //    HandleEventHealthChanged();
//    //}

//    //public void HandleEventTakeDamage(float amount)
//    //{
//    //    OnTakeDamage?.Invoke(amount);
//    //    HandleEventHealthChanged();
//    //}

//    //public void HandleEventDie()
//    //{
//    //    History.HandleKDA();
//    //    OnDie(this);
//    //}
//    //#endregion


//    #region Animations Handle
//    //public void HandleAnimationMove()
//    //{
//    //    animator.SetBool("isMove", true);
//    //}

//    //public void HandleAnimationStop()
//    //{
//    //    animator.SetBool("isMove", false);
//    //}

//    //public IEnumerator PlayAnimationOneShot(AnimationCharacterType animationType)
//    //{
//    //    animator.SetBool("is" + animationType.ToString(), true);

//    //    //yield return new WaitForSeconds(GetLengthAnimation(animationType));
//    //    yield return new WaitForSeconds(0.375f);
//    //    animator.SetBool("is" + animationType.ToString(), false);
//    //}

//    //public float GetLengthAnimation(AnimationCharacterType animationType)
//    //{
//    //    foreach (var clip in animationClips)
//    //    {
//    //        if (clip.name == animationType.ToString())
//    //        {
//    //            return clip.length;
//    //        }
//    //    }

//    //    return 0;
//    //}
//    //#endregion


//    //public static float GetTimeRevialHero()
//    //{
//    //    float FlagTimeRevialHeroMax = 12 * 60;   //seconds
//    //    float FlagTimeRevialHeroMin = 30;        //seconds

//    //    float ValueTimeRevialHeroMax = 60f;      //seconds
//    //    float ValueTimeRevialHeroMin = 10f;      //seconds

//    //    float secondCurrent = (float)GameManager.Instance.BattleTime.Elapsed.TotalSeconds;

//    //    if (secondCurrent < FlagTimeRevialHeroMin)
//    //    {
//    //        return ValueTimeRevialHeroMin;
//    //    }
//    //    else if (secondCurrent > FlagTimeRevialHeroMax)
//    //    {
//    //        return ValueTimeRevialHeroMax;
//    //    }
//    //    else
//    //    {
//    //        float percent = (secondCurrent - FlagTimeRevialHeroMin) / (FlagTimeRevialHeroMax - FlagTimeRevialHeroMin);
//    //        return percent * (ValueTimeRevialHeroMax - ValueTimeRevialHeroMin) + ValueTimeRevialHeroMin;
//    //    }
//    //}

//    //public IEnumerator PlaySoundMovement()
//    //{
//    //    yield return new WaitForSeconds(UnityEngine.Random.Range(15f, 25f));

//    //    if (NameID == "Roken")
//    //    {
//    //        audioSource.PlayOneShot(sound.GetRandomSound(CharacterSound.TypeSound.Movement));
//    //        StartCoroutine(PlaySoundMovement());
//    //    }
//    //}

//}