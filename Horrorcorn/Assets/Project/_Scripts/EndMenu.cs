using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    void Start()
    {
        gameObject.SetActive(false);
        Keys.WinEvent += EnableEndMenu;
    }

    private void EnableEndMenu()
    {
        gameObject.SetActive(true);
        TimeManager.SetPause(gameObject);
        PlayerCamera.UnlockCursor();
    }

    public void TryAgainClicked()
    {
        TimeManager.RemovePause(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
