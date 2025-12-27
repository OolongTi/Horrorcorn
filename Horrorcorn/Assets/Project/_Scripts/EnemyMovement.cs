using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateInterval = 0.1f;
    
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
    }
    
    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateInterval);
        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            
            yield return wait;
        }
    }
}
