using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public enum TypeCharacter
{
    Unset,
    Hero,
    Legion,
    SmallCreep,
    MediumCreep,
    LargeCreep,
    Tower,
}

public enum TeamCharacter
{
    Unset,
    Blue,
    Red,
    Natural,
}

[RequireComponent(typeof(StatsCharacter), typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    //Hide in inpestor and add properties

    public float rangeAttack;

    [Header("Information Character")]
    [SerializeField] new string name;
    [SerializeField] string id;
    [SerializeField] TypeCharacter typeCharacter;
    [SerializeField] TeamCharacter team;

    public StatsCharacter stats;

    public event Action<Health> OnHealthChanged;
    public event Action<Mana> OnManaChanged;
    public event Action<Level> OnLevelChanged;

    public string Name { get; private set; }
    public string ID { get; private set; }

    private void Start()
    {
        StatsBar.AddFor(this, typeCharacter, team);

        stats = GetComponent<StatsCharacter>();
        stats.SetDefault();

        OnHealthChanged?.Invoke(stats.health);
        OnManaChanged?.Invoke(stats.mana);
        OnLevelChanged?.Invoke(stats.level);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, rangeAttack);
    }


    public void GenerateID()
    {
        ID = Guid.NewGuid().ToString();
    }

    public void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeAttack);


        foreach (var collider in colliders)
        {

            if (collider.GetComponent<Character>() != null && collider != this.GetComponent<Collider>())
            {
                Debug.Log(string.Format("{0} attack {1}", gameObject.name, collider.gameObject.name));
                collider.GetComponent<Character>().TakeDamage((int)(stats.physicalDamage.Current));
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(string.Format("{0} take {1} damage", gameObject.name, damage));

        stats.health.Current -= damage;
        OnHealthChanged(stats.health);
    }

    public void HealingHealth(float factorHealing)
    {
        stats.health.Healing((int)(stats.health.Max * factorHealing));
        OnHealthChanged(stats.health);
    }

    public void Glide(Vector3 direction, float duration)
    {
        transform.DOLookAt(transform.position + direction, 0.1f);

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.DOMove(transform.position + direction, duration).SetEase(Ease.InOutCubic).SetLoops(2, LoopType.Yoyo);
    }

    public void RotateCrazy(int loop, float durationForLoop)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.DORotate(new Vector3(0, 360), durationForLoop).SetLoops(loop, LoopType.Incremental);

    }
}