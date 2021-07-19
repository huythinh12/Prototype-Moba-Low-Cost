using UnityEngine;
using UnityEngine.AI;

public class AgentNavMesh : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    private NavMeshAgent navMeshAgent;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = targetTransform.position;
    }
}
