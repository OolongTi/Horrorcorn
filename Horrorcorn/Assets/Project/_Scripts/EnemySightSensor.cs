using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightSensor : MonoBehaviour
{
    public Transform eyeHeight;
    
    public static event Action PlayerSighted;

    private void Start()
    {
        EnemySightTrigger.PlayerInTrigger += PlayerInRange;
    }
    
    private void OnDestroy()
    {
        EnemySightTrigger.PlayerInTrigger -= PlayerInRange;
    }

    private void PlayerInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;
        
        Vector3 direction = (player.transform.position - eyeHeight.transform.position).normalized;
        
        RaycastHit hit;
        
        if (Physics.Raycast(eyeHeight.position, direction, out hit, 50f))
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("PlayerSensor"))
            {
                PlayerSighted?.Invoke();
                
                Debug.DrawRay(eyeHeight.position, direction * hit.distance, Color.green);
            }
            else
            {
                Debug.DrawRay(eyeHeight.position, direction * hit.distance, Color.red);
            }
        }
    }
}
