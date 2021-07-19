using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
public class AgentMinionCreep : MonoBehaviour
{

    [SerializeField]
    private Transform targetTransformLeft;
    [SerializeField]
    private Transform targetTransformRight;

    Sequence sequence;
    private void Start()
    {
        sequence.Append(transform.DOMoveX(targetTransformLeft.position.x, 6).SetLoops(-1, LoopType.Yoyo));

        sequence.Play();


    }


}
