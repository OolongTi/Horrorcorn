using System;
using UnityEngine;

public class EndGoalScript : MonoBehaviour
{
    public static event Action ReachedGoalEvent;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ReachedGoalEvent?.Invoke();
        }
    }
}
