using System;
using UnityEngine;

public class SoundScriptKeys : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event soundKey;

    void Start()
    {
        PickupSensor.PickupCollected += PlayKeySound;
    }

    private void OnDestroy()
    {
        PickupSensor.PickupCollected -= PlayKeySound;
    }

    private void PlayKeySound(Pickup pickup)
    {
        soundKey.Post(gameObject);
    }
}
