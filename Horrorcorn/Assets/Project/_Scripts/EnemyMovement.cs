
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateInterval = 0.1f;
    private bool following = false;
    
    private bool hasReachedDestination;
    
    private NavMeshAgent agent;
    private Coroutine followCoroutine;

    private EnemyAnimation animation;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<EnemyAnimation>();
    }
    
    public void PlayerSighted()
    {
        if (!following)
        {
            followCoroutine = StartCoroutine(FollowTarget());
            following = true;
        }
    }

    public void PlayerGone()
    {
        if (following)
        {
            StopCoroutine(followCoroutine);

            if (target != null)
            { 
                agent.SetDestination(target.transform.position);
            }
            following = false;
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, agent.destination);

        if (distance <= 1f && !hasReachedDestination)
        {
            hasReachedDestination = true;
            animation.Idle();
            
        }
        else if (distance > 1.5f)
        {
            hasReachedDestination = false;
        }
    }
    
    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateInterval);
        while (true)
        {
            agent.SetDestination(target.transform.position);
            
            yield return wait;
        }
    }
}