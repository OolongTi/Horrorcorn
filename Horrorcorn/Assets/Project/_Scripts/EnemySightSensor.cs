using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public Transform eyeHeight;
    
    private EnemyMovement movement;
    private EnemyAnimation animation;

    private void Awake()
    {
        movement = GetComponent<EnemyMovement>();
        animation = GetComponentInChildren<EnemyAnimation>();
    }

    public void PlayerInRange(GameObject player)
    {
        if (player == null) return;
        
        Vector3 direction = (player.transform.position - eyeHeight.transform.position).normalized;
        
        RaycastHit hit;
        
        if (Physics.Raycast(eyeHeight.position, direction, out hit, 50f))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("PlayerSensor"))
            {
                movement.PlayerSighted();
                animation.ChasePlayer();
                Debug.DrawRay(eyeHeight.position, direction * hit.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(eyeHeight.position, direction * hit.distance, Color.red);
            }
        }
    }
}
