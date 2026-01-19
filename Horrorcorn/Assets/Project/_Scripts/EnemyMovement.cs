
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

    public ParticleSystem Dust;
    private ParticleSystem.EmissionModule emission;
    private bool hasEmission;
    
    
    public float fastEmissionRate = 10f; 
    public float slowEmissionRate = 3f;
    
    

    [SerializeField] private Transform enemyPos1;
    [SerializeField] private Transform enemyPos2;
    private bool goingToPos1 = false;
    private bool reachedIdleDestination = false;

    [SerializeField] private AK.Wwise.Event enemySoundsEvent;
    [SerializeField] private AK.Wwise.Switch idleSwitch;
    [SerializeField] private AK.Wwise.Switch chaseSwitch;

    private void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<EnemyAnimation>();
        
        if (Dust != null)
        {
            emission = Dust.emission;
            hasEmission = true;
        }
    }

    private void Start()
    {
        enemySoundsEvent.Post(gameObject);
    }

    public void PlayerSighted()
    {
        if (!following)
        {
            chaseSwitch.SetValue(gameObject);
            agent.speed = 5f;
            followCoroutine = StartCoroutine(FollowTarget());
            following = true;
        }
    }

    public void PlayerGone()
    {
        if (following)
        {
            idleSwitch.SetValue(gameObject);
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
        var emission = Dust.emission;

        agent.SetDestination(enemyPos1.transform.position);
        animation.IdleWalk();
        if (hasEmission)
        {
            emission.rateOverTime = slowEmissionRate;
        }
    }
    private void IdleWalkTo2()
    {

        agent.SetDestination(enemyPos2.transform.position);
        animation.IdleWalk();
        if (hasEmission)
        {
            emission.rateOverTime = slowEmissionRate;
        }
    }
    

    void Update()
    {

        if(!following && hasReachedDestination)
        {
            agent.speed = 2f;
            if (goingToPos1 && reachedIdleDestination)
            {
                IdleWalkTo1();
                if (hasEmission)
                {
                    emission.rateOverTime = slowEmissionRate;
                }
            }
            else if (!goingToPos1 && reachedIdleDestination)
            {
                IdleWalkTo2();
                if (hasEmission)
                {
                    emission.rateOverTime = slowEmissionRate;
                }
            }
            
            float idleDistance = Vector3.Distance(transform.position, agent.destination);
            if (idleDistance <= 5f && !reachedIdleDestination) 
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
            if (hasEmission)
            {
                emission.rateOverTime = fastEmissionRate;
            }
            
            yield return wait;
        }
    }
}
