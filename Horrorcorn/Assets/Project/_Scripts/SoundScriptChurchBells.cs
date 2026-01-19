using UnityEngine;

public class SoundScriptChurchBells : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event churchBellEvent;
    void Start()
    {
        Keys.AlleKeysEvent += PlayChurchBells;
    }
    
    private void PlayChurchBells()
    {
        churchBellEvent.Post(gameObject);
    }

    void OnDestroy()
    {
        Keys.AlleKeysEvent -= PlayChurchBells;
    }

    
    void Update()
    {
        
    }
}
