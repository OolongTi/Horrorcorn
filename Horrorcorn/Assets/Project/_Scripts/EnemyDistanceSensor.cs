using System;
using UnityEngine;

public class EnemyDistanceSensor : MonoBehaviour
{
    private EnemyMovement movement;

    private void Awake()
    {
        movement = GetComponentInParent<EnemyMovement>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            movement.PlayerGone();
        }
    }
}
