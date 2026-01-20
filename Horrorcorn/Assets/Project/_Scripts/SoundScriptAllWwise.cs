using System;
using UnityEngine;

public class SoundScriptAllWwise : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event looseSound;
    [SerializeField] private AK.Wwise.Event winSound;
    private void Start()
    {
       DontDestroyOnLoad(this);
       EnemyKillScript.LooseEvent += PlayLooseSound;
       VoidScript.FellIntoVoid += () => PlayLooseSound("Void");
       Keys.WinEvent += PlayWinSound;
    }

    private void PlayLooseSound(string strng)
    {
        looseSound.Post(gameObject);
    }
    
    private void PlayWinSound(string strng)
    {
        winSound.Post(gameObject);
    }

    

    private void OnDestroy()
    {
        EnemyKillScript.LooseEvent -= PlayLooseSound;
        VoidScript.FellIntoVoid -= () => PlayLooseSound("Void");
        Keys.WinEvent -= PlayWinSound;
        AkUnitySoundEngine.StopAll();
    }
}
