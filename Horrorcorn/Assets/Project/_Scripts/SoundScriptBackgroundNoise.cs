using System;
using UnityEngine;

public class SoundScriptBackgroundNoise : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event backgroundNoiseEvent;
    [SerializeField] private AK.Wwise.State gameSwitch;
    [SerializeField] private AK.Wwise.State menuSwitch;

    private void Start()
    {
        backgroundNoiseEvent.Post(gameObject);
        StartMenu.MenuOpen += SwitchToMenu;
        StartMenu.MenuClosed += SwitchToGame;
    }
    
    private void SwitchToGame()
    {
        gameSwitch.SetValue();
    }
    
    private void SwitchToMenu()
    {
        menuSwitch.SetValue();
    }

    private void OnDestroy()
    {
        StartMenu.MenuOpen -= SwitchToMenu;
        StartMenu.MenuClosed -= SwitchToGame;
    }
}
