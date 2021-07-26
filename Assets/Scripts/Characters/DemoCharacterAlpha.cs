//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using DG.Tweening;

//// xu ly toan bo hanh vi character 
////[RequireComponent(typeof(StatsCharacter), typeof(Rigidbody), typeof(Collider))]
//public class DemoCharacterAlpha : MonoBehaviour
//{

//    //Hide in inpestor and add properties
//    public List<Ability> abilities = new List<Ability>();

//    public float rangeAttack;

//    [Header("Information Character")]
//    [SerializeField] new string name;
//    [SerializeField] string id;
//    [SerializeField] TypeCharacter typeCharacter;
//    [SerializeField] TeamCharacter team;

//    [SerializeField]
//    private MatchData matchdata;
//    private StatsCharacter stats;
//    private Animator animator;

//    public event Action<Health> OnHealthChanged;
//    public event Action<Mana> OnManaChanged;
//    public event Action<Level> OnLevelChanged;

//    public string Name { get; private set; }
//    public string ID { get; private set; }
//    public TeamCharacter Team { get => team; set => team = value; }

//    private void Awake()
//    {

//        abilities.Add(new NoahAlphaAbility());
//        abilities.Add(new NoahBetaAbility());
//        abilities.Add(new NoahUltimateAbility());
//    }


//    private void Start()
//    {
//        StatsBar.AddFor(this, typeCharacter, team);

//        animator = GetComponent<Animator>();
//        stats = GetComponent<StatsCharacter>();
//        stats.SetDefault();

//        OnHealthChanged += CheckDie;

//        OnHealthChanged?.Invoke(stats.health);
//        OnManaChanged?.Invoke(stats.mana);
//        OnLevelChanged?.Invoke(stats.level);
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            Attack();
//        }

//        if (Input.GetKeyDown(KeyCode.KeypadEnter))
//        {
//            TakeDamage(10);
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawWireSphere(transform.position, rangeAttack);
//    }

//    public void GenerateID()
//    {
//        ID = Guid.NewGuid().ToString();
//    }

//    public IEnumerator Revival()
//    {
//        yield return new WaitForSeconds(matchdata.timeToRevivalHeroMin);

//        //ToDo: Switch case TeamCharacter type => position = point revival based on team
//    }

//    public void CheckDie(Health health)
//    {
//        if (health.Current <= 0)
//        {
//            animator.SetTrigger("Death");
//            //ToDo:
//            //coroutine
//            //StartCoroutine(Revival());
//            //gameObject.SetActive(false);
//        }

//    }

//    public void Attack()
//    {
//        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeAttack);


//        foreach (var collider in colliders)
//        {

//            if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
//            {
//                Debug.Log(string.Format("{0} attack {1}", gameObject.name, collider.gameObject.name));
//                collider.GetComponent<Character>().TakeDamage((int)(stats.physicalDamage.Current));
//            }
//        }
//    }

//    public void TakeDamage(int damage)
//    {
//        Debug.Log(string.Format("{0} take {1} damage", gameObject.name, damage));

//        stats.health.Current -= damage;
//        OnHealthChanged(stats.health);
//    }

//    public void HealingHealth(float amountHeaing, float factorHealing)
//    {
//        stats.health.Healing((int)(amountHeaing + stats.health.Max * factorHealing));
//        OnHealthChanged(stats.health);
//    }


//    /// <summary>
//    /// test skill 
//    /// </summary>
//    /// <param name="direction"></param>
//    /// <param name="duration"></param>
//    public void Glide(Vector3 direction, float duration)
//    {
//        transform.DOLookAt(transform.position + direction, 0.1f);

//        transform.DOMove(transform.position + direction, duration).SetEase(Ease.InOutCubic);
//        transform.DOMove(transform.position - direction * 0.5f, duration * 0.65f).SetEase(Ease.InOutCubic);
//        //transform.DOMove(transform.position + direction, duration).SetEase(Ease.InOutQuint);
//    }

//    public void RotateCrazy(int loop, float durationForLoop)
//    {
//        Rigidbody rigidbody = GetComponent<Rigidbody>();
//        rigidbody.DORotate(new Vector3(0, 360), durationForLoop).SetLoops(loop, LoopType.Incremental);

//    }
//}