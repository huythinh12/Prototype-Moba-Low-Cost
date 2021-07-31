using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovementController
{
    Character self;

    public CharacterMovementController(Character character)
    {
        this.self = character;
    }

    public IEnumerator LookAtPosition(Vector3 position)
    {
        float duration = 0f;
        Ease easeLookAt = Ease.Linear;

        self.transform.DOLookAt(position, duration).SetEase(easeLookAt);

        yield return null;
    }


    public void Move(Vector3 direction)
    {
        if (true)//self.IsAlive)
        {
            self.StartCoroutine(self.MovementController.LookAtPosition(self.transform.position + direction));
            self.rigidbody.velocity = direction * self.Stats.MovementSpeed.Value;

            self.HandleAnimationMove();
        }
    }

    public void StopMove()
    {
        self.rigidbody.velocity = Vector3.zero;

        self.HandleAnimationStop();
    }

}
