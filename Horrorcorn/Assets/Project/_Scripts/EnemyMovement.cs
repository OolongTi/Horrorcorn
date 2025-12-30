
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

    [SerializeField] private Transform enemyPos1;
    [SerializeField] private Transform enemyPos2;
    private bool goingToPos1 = false;
    private bool reachedIdleDestination = false;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<EnemyAnimation>();
    }
    
    public void PlayerSighted()
    {
        if (!following)
        {
            agent.speed = 5f;
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
    
    private void IdleWalkTo1()
    {
        agent.SetDestination(enemyPos1.transform.position);
        animation.IdleWalk();
    }
    private void IdleWalkTo2()
    {
        agent.SetDestination(enemyPos2.transform.position);
        animation.IdleWalk();
    }
    

    void Update()
    {
        
        if(!following && hasReachedDestination)
        {
            agent.speed = 2f;
            if (goingToPos1 && reachedIdleDestination)
            {
                IdleWalkTo1();
            }
            else if (!goingToPos1 && reachedIdleDestination)
            {
                IdleWalkTo2();
            }
            
            float idleDistance = Vector3.Distance(transform.position, agent.destination);
            if (idleDistance <= 1f && !reachedIdleDestination) 
            {
                reachedIdleDestination = true;
            }
            else if (idleDistance > 1.5f)
            {
                reachedIdleDestination = false;
                goingToPos1 = !goingToPos1;
            }
        }



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