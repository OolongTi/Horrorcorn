using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int keys = 0;
    [SerializeField] private TextMeshProUGUI keysText;

    void Start()
    {
        PickupSensor.PickupCollected += KeyCollected;
    }

    private void KeyCollected(Pickup pickup)
    {
        keys++;
        keysText.text = $"Keys: {keys}/4";
    }
    
    void Update()
    {
        
    }
}
