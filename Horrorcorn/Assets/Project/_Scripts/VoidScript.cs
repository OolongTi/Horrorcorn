using System;
using UnityEngine;

public class VoidScript : MonoBehaviour
{
    public static event Action FellIntoVoid;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FellIntoVoid?.Invoke();
        }
    }
}
