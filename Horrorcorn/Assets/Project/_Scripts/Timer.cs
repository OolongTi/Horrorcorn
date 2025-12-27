using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0;
    private int secondsTime = 0;
    [SerializeField] private TextMeshProUGUI timerText;
    
    void Update()
    {
        time += Time.deltaTime;
        secondsTime = (int)time;
        timerText.text = $"Time: {secondsTime}";
    }
}
