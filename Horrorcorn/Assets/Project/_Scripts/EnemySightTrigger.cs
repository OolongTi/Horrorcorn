using UnityEngine;
using System;

public class EnemySightTrigger : MonoBehaviour
{
    private EnemySightSensor sensor;

    private void Awake()
    {
        sensor = GetComponentInParent<EnemySightSensor>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sensor.PlayerInRange(other.gameObject);
        }
    }
}

