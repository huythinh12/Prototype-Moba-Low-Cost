using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(CharacterStats))]
public class Character : MonoBehaviour
{
    public static readonly Vector3 revivalPositionBlue = new Vector3(-70f, 0f, 4f);
    public static readonly Vector3 revivalPositionRed = new Vector3(70f, 0f, 4f);
    public static readonly float AttackDetectionRange = 5f;

    private MatchData matchdata;

    [HideInInspector] public CharacterStats Stats;
    [SerializeField] private new string name;
    [SerializeField] private string id;
    [SerializeField] private TypeCharacter typeCharacter;
    [SerializeField] private TeamCharacter team;
    private Sprite icon;

    bool isAlive = true;

    private new Rigidbody rigidbody;
    private new Collider collider;
    private Animator animator;
    private StatsBar statsBar;

    public List<Ability> abilities = new List<Ability>();

    public string Name { get => name; set => name = value; }
    public string ID { get => id; set => id = value; }
    public TypeCharacter TypeCharacter { get => typeCharacter; set => typeCharacter = value; }
    public TeamCharacter Team { get => team; set => team = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    public event Action<int> OnLevelChanged;
    public event Action<CharacterStats> OnHealthChanged;
    public event Action<CharacterStats> OnManaChanged;
    public event Action<float> OnTakeDamage;
    public event Action<float> OnHealingHealth;

    private void Awake()
    {
        AddAbility();
    }

    void Start()
    {
        GetComponentsDefault();
        AddStatsBar();
        SetStatsDefault();
        SetIcon();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack();
            Debug.Log("A pressed!");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            HealingMana(15f);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Revival();
            //TakeDamage(this, DamageType.Physical, 0.15f, StatPercentType.Max);
        }
    }




    //Default settings
    protected virtual void AddAbility()
    {
        //abilities.Add(new ExampleAbility());
    }


    void GetComponentsDefault()
    {
        Stats = GetComponent<CharacterStats>();
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void AddStatsBar()
    {
        statsBar = StatsBar.AddFor(this, TypeCharacter, team);
    }

    public void SetStatsDefault()
    {
        Stats.SubscribeOnLevelChange(this);

        Stats.ResetHealthCurrent();
        Stats.ResetManaCurrent();

        HandleEventGeneralStatsChanged();
    }

    public void SetIcon()
    {
        icon = Resources.Load<Sprite>(BaseInformation.GenerateResourcePath(ResourceType.Icon, this));
    }

    private Vector3 GetRevivalPosition()
    {
        switch (team)
        {
            case TeamCharacter.Natural:
                return Vector3.zero;
            case TeamCharacter.Blue:
                return revivalPositionBlue;
            case TeamCharacter.Red:
                return revivalPositionRed;
            default:
                return Vector3.zero;
        }
    }
    //End



    //Ability behavior
    public void Move(Vector3 direction)
    {
        if (isAlive)
        {
            transform.DOLookAt(transform.position + direction, 0f);
            rigidbody.velocity = direction * Stats.MovementSpeed.Value;

            animator.SetBool("isMoving", true);
        }
    }

    public void MoveToTarget(Character character)
    {
        if (isAlive)
        {
            Vector3 direction = character.transform.position - this.transform.position;
            Move(direction.normalized * 0.85f);
        }
    }

    public void StopMove()
    {
        rigidbody.velocity = Vector3.zero;

        animator.SetBool("isMoving", false);
    }

    public Character FindTargetInDetectionRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, AttackDetectionRange);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
            {
                return collider.GetComponent<Character>();
            }
        }

        return null;
    }

    public Character FindTargetInRangeAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Stats.RangeAttack.Value);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
            {
                return collider.GetComponent<Character>();
            }
        }

        return null;
    }


    public void Attack()
    {
        //StopMove();

        //Character target = FindTargetInDetectionRange();


        //if (target == null)
        //{
        //    Debug.Log("Khong co muc tieu!");
        //}
        //else
        //{
        //    //while (Vector3.Distance(this.transform.position, target.transform.position) >= Stats.RangeAttack.Value / 2)
        //    //{
        //    //    MoveToTarget(target);
        //    //}

        //    StopMove();
        //}


        if (isAlive)
        {
            //animator.SetBool("isAttack", true);

            //StartCoroutine(PlayAnimationOneShot(AnimationType.Attack));

            Collider[] colliders = Physics.OverlapSphere(transform.position, Stats.RangeAttack.Value);


            foreach (var collider in colliders)
            {

                if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
                {
                    if (collider.GetComponent<Character>().team != this.team)
                    {
                        Debug.Log(string.Format("{0} attack {1}", gameObject.name, collider.gameObject.name));
                        collider.GetComponent<Character>().TakeDamage(this, DamageType.Physical, 10f);
                    }
                }
            }
        }
    }

    public void Glide(Vector3 direction, float duration, Ease easeGlide = Ease.InOutCubic)
    {
        if (isAlive)
        {
            transform.DOLookAt(transform.position + direction, 0f);
            rigidbody.DOMove(transform.position + direction, duration).SetEase(easeGlide);
        }
    }

    public void JumpTo(Vector3 direction, float duration)
    {
        StopMove();

        transform.DOLookAt(transform.position + direction, 0f);
        rigidbody.DOJump(transform.position + direction, 10f, 1, duration).SetEase(Ease.InOutExpo);

        StopMove();
    }

    public void Revival()
    {
        transform.position = GetRevivalPosition();
        isAlive = true;

        statsBar.gameObject.SetActive(true);
        collider.enabled = true;

        Stats.ResetHealthCurrent();
        Stats.ResetManaCurrent();
        HandleEventGeneralStatsChanged();
    }

    public void CheckDie()
    {
        if (Stats.HealthCurrent <= 0)
        {
            isAlive = false;

            statsBar.gameObject.SetActive(false);
            collider.enabled = false;

            animator.SetTrigger("Death");
            animator.SetBool("isDeath", true);
            //ToDo:
            //coroutine
            //StartCoroutine(Revival());
            //gameObject.SetActive(false);
        }
    }
    //End


    //Interacting with stat
    public bool UseMana(float mana)
    {
        if (Stats.ManaCurrent - mana >= 0)
        {
            Stats.ManaCurrent -= mana;
            HandleEventManaChanged();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LevelUp()
    {
        Stats.Level++;
        HandleEventLevelChanged();
    }

    public void HealingMana(float amount)
    {
        if (isAlive)
        {
            Stats.ManaCurrent += amount;
            HandleEventManaChanged();
        }
    }

    public void HealingHealth(float amount)
    {
        if (isAlive)
        {
            float healthHealed = amount;
            Stats.HealthCurrent += healthHealed;

            OnHealingHealth?.Invoke(healthHealed);
            HandleEventHealthChanged();

            Debug.Log(string.Format("{0} was healing {1} health", name, healthHealed));
        }
    }

    public void HealingHealth(float percentMaxHealth, StatPercentType statPercentType)
    {
        if (isAlive)
        {
            float healthHealed = Stats.GetPercentHealth(percentMaxHealth, statPercentType);
            HealingHealth(healthHealed);
        }
    }

    public void TakeDamage(Character characterDealDamage, DamageType damageType, float amountDamage)
    {
        if (isAlive)
        {
            float damageTaken = amountDamage * CharacterStats.GetFinalPercentDamageTaken(damageType, characterDealDamage, this);
            damageTaken *= CharacterStats.GetFinalPercentDamageTaken(damageType, characterDealDamage, this);

            Stats.HealthCurrent -= damageTaken;

            OnTakeDamage?.Invoke(damageTaken);
            HandleEventHealthChanged();

            Debug.Log(string.Format("{0} was taken {1} damage!", name, damageTaken));
        }
    }

    public void TakeDamage(Character characterDealDamage, DamageType damageType, float percentMaxHealth, StatPercentType statPercentType)
    {
        if (isAlive)
        {
            float damageTaken = Stats.GetPercentHealth(percentMaxHealth, statPercentType);
            TakeDamage(characterDealDamage, damageType, damageTaken);
        }
    }
    //End


    //Handle Event
    public void HandleEventGeneralStatsChanged()
    {
        HandleEventHealthChanged();
        HandleEventManaChanged();
        HandleEventLevelChanged();
    }

    public void HandleEventHealthChanged()
    {
        OnHealthChanged?.Invoke(Stats);
        CheckDie();
    }

    public void HandleEventManaChanged()
    {
        OnManaChanged?.Invoke(Stats);
    }

    public void HandleEventLevelChanged()
    {
        OnLevelChanged?.Invoke(Stats.Level);
    }
    //End


    //public IEnumerator PlayAnimationOneShot(AnimationType animationType)
    //{
    //    animator.SetBool("is" + animationType.ToString(), true);

    //    yield return new WaitForSeconds(GetLengthAnimation(animationType));
    //    animator.SetBool("is" + animationType.ToString(), false);
    //}

    //public float GetLengthAnimation(AnimationType animationType)
    //{
    //    foreach (var clip in clips)
    //    {
    //        if (clip.name == animationType.ToString())
    //        {
    //            return lengthAttackAnimation = clip.length;
    //        }
    //    }

    //    return 0;
    //}
}
