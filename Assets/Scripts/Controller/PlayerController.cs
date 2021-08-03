using UnityEngine;

[DefaultExecutionOrder(1000)]
public class PlayerController : MonoBehaviour
{
    MovementJoystick movementJoystick;
    Character character;

    bool isDeath = false;

    private void Start()
    {
        character = GetComponent<Character>();

        movementJoystick = GameObject.FindObjectOfType<MovementJoystick>();
        movementJoystick.OnIndicationDrag += Movement;
        movementJoystick.OnIndicationDone += Stop;
    }

    private void Movement(MovementJoystick movementJoystick)
    {
        //character.MovementController.Move(JoystickMath.OxzIndicatorNormalized(JoystickMath.ConvertToOxzIndicator(movementJoystick.latePoint)));
    }

    private void Stop(MovementJoystick movementJoystick)
    {
        character.MovementController.StopMove();
    }

    private void AnimationWithAction()
    {
        ////test Death with key R 
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    isDeath = true;
        //    anim.SetTrigger("Death");
        //    anim.SetBool("isDeath", isDeath);
        //    return;
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    anim.SetBool("isAttack", true);
        //}
        //else if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    anim.SetBool("isAttack", false);

        //}


    }
}
