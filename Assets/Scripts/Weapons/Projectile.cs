using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementProjectileMode
{
    Linear,
    TrackingTarget,
    PositionSpecified,
}

public enum ThroughtProjectileMode
{
    FristTarget,
    OnlyTarget,
    AllTarget,
}

public class Projectile : MonoBehaviour
{
    new private Rigidbody rigidbody;
    private Character self;
    private float forceMovement = 10f;

    MovementProjectileMode movementMode = MovementProjectileMode.Linear;
    ThroughtProjectileMode throughtMode = ThroughtProjectileMode.OnlyTarget;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    public void Tirer(Vector3 positionTarget, DamageType damageType)
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(positionTarget.normalized * forceMovement, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        Character characterHit = other.GetComponent<Character>();

        if (characterHit == null)
        {

        }
        else
        {
            if (IsTarget(characterHit))
            {

            }
        }
    }


    private bool IsTarget(Character characterHit)
    {
        if (self.team != characterHit.team)
            return true;
        return false;
    }
}
