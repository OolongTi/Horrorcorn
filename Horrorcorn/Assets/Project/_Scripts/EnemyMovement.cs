
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
    
    private NavMeshAgent agent;
    private Coroutine followCoroutine;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        EnemySightSensor.PlayerSighted += playerSighted;
        EnemyDistanceSensor.PlayerGone += playerGone;
    }

    private void playerSighted()
    {
        if (!following)
        {
            followCoroutine = StartCoroutine(FollowTarget());
            following = true;
        }
    }

    private void playerGone()
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
