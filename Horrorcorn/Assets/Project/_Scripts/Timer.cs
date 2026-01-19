using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time = 0;
    private int secondsTime = 0;
    [SerializeField] public TextMeshProUGUI timerText;

    
    private void Start()
    {
        Keys.WinEvent += giveTimer;
    }

    void Update()
    {
        time += Time.deltaTime;
        secondsTime = (int)time;
        timerText.text = $"Time: {secondsTime}";
    }

    private void giveTimer(string obj)
    {

    }

    private void OnDestroy()
    {
        Keys.WinEvent -= giveTimer;
    }
}
