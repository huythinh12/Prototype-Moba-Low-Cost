using UnityEngine;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private VariableJoystick variableJoystick;
    [SerializeField]
    private Rigidbody rigidbodyPlayer;
    [SerializeField]
    private float rotateSpeedMovement = 0.1f;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        if (variableJoystick.Vertical == 0 && variableJoystick.Horizontal == 0)
        {
            rigidbodyPlayer.velocity = Vector3.zero;
        }
        else
        {
            rigidbodyPlayer.velocity = direction * speed;
            transform.DOLookAt(direction + transform.position, 0.1f);
        }

        AnimationWithAction();
    }

    private void AnimationWithAction()
    {
        if (rigidbodyPlayer.velocity.x != 0 || rigidbodyPlayer.velocity.y != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);

        }// hold to attack and release to stop
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("isAttack",true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isAttack", false);

        }
    }
}
