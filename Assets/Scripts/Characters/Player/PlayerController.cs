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
    private Transform pointDirection;
    [SerializeField]
    private float rotateSpeedMovement = 0.1f;

    public void Update()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;

        rigidbodyPlayer.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);
        
        
            transform.DOLookAt(direction + transform.position, 0.1f);

        
    }
}
