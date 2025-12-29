using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Keys : MonoBehaviour
{
    public int keys = 0;
    [SerializeField] private TextMeshProUGUI keysText;

    public static event Action WinEvent;

    void Start()
    {
        PickupSensor.PickupCollected += KeyCollected;
    }

    private void KeyCollected(Pickup pickup)
    {
        keys++;
        keysText.text = $"Keys: {keys}/4";
        if (keys == 4)
        {
            WinEvent?.Invoke();
        }
    }
    
    void Update()
    {
        
    }
}
