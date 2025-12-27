using UnityEngine;

public class StartMenu : MonoBehaviour
{
    void OnEnable()
    {
        TimeManager.SetPause(gameObject);
    }

    void OnDisable()
    {
        TimeManager.RemovePause(gameObject);
    }

    public void StartClicked()
    {
        PlayerCamera.LockCursor();
        gameObject.SetActive(false);
    }
}
