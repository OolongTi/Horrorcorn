using UnityEngine;
using System;

public class EnemySightTrigger : MonoBehaviour
{
    public static event Action PlayerInTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInTrigger?.Invoke();
        }
    }
}

