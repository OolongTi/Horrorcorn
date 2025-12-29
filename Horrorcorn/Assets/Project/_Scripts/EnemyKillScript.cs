using System;
using UnityEngine;

public class EnemyKillScript : MonoBehaviour
{
    public static event Action LooseEvent;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LooseEvent?.Invoke();
        }
    }
}
