using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Keys : MonoBehaviour
{
    public int keys = 0;
    [SerializeField] private TextMeshProUGUI keysText;

    public static event Action<string> WinEvent;
    public static event Action AlleKeysEvent;

    void Start()
    {
        PickupSensor.PickupCollected += KeyCollected;
        EndGoalScript.ReachedGoalEvent += ReachedGoal;
    }

    private void KeyCollected(Pickup pickup)
    {
        keys++;
        keysText.text = $"Keys: {keys}/4";
        if (keys == 4)
        {
            AlleKeysEvent?.Invoke();
        }
    }
    
    public void ReachedGoal()
    {
        if (keys == 4)
        {
            WinEvent?.Invoke("You Won!");
        }
    }
    
    void OnDestroy()
    {
        PickupSensor.PickupCollected -= KeyCollected;
    }
    
    void Update()
    {
        
    }
}
