using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        // Disable the NavMeshAgent by default since no movement is needed
        agent.enabled = false;
    }

    // Remove or disable Update method since it's not required if customers are not moving
    private void Update()
    {
        if (target != null && agent.enabled)
        {
            agent.SetDestination(target.position);
        }
    }

    public void GotoTable(Transform newTarget)
    {
        target = newTarget;
        agent.enabled = true;  // Enable agent if we need to move in the future
    }
}
