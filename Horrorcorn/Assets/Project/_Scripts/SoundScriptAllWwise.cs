using System;
using UnityEngine;

public class SoundScriptAllWwise : MonoBehaviour
{
    private void Start()
    {
       
    }

    private void OnDestroy()
    {
        AkUnitySoundEngine.StopAll();
    }
}
