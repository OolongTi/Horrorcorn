using System;
using UnityEngine;

public class EnemyDistanceSensor : MonoBehaviour
{
    public static event Action PlayerGone;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerGone?.Invoke();
        }
    }
}
