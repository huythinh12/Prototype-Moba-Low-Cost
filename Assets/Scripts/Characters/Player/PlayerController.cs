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

    private Animator anim;

    bool isDeath = false;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        //check death
        if (isDeath) {
            rigidbodyPlayer.velocity = Vector3.zero;
            return; 
        }

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
        //test Death with key R 
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("Death");
            isDeath = true;
            return;
        }

        if (rigidbodyPlayer.velocity.x != 0 || rigidbodyPlayer.velocity.y != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);

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
