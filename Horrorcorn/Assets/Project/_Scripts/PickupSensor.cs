using System;
using UnityEngine;

public class PickupSensor : MonoBehaviour
{
    public static event Action<Pickup> PickupCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KeyPickup"))
        {
            Pickup pickup = other.GetComponent<Pickup>();
            if (pickup != null)
            {
                PickupCollected?.Invoke(pickup);
            }
        }
    }
}
