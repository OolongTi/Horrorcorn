using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinOrLoseText;
    private static string PreviousEndMessage = "";
    void Start()
    {
        Keys.WinEvent += EnableEndMenu;
        EnemyKillScript.LooseEvent += EnableEndMenu;
        if (WinOrLoseText != null && PreviousEndMessage != "")
        {
            WinOrLoseText.text = PreviousEndMessage;
            if(PreviousEndMessage == "You Won!")
                WinOrLoseText.color = Color.green;
            else
            {
                WinOrLoseText.color = Color.red; 
            }
        }
    }
    
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
    
    public void QuitClicked()
    {
        Application.Quit();
    }
    
    private void EnableEndMenu(string message)
    {
        TimeManager.SetPause(gameObject);
        PlayerCamera.UnlockCursor();
        PreviousEndMessage = message;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        WinOrLoseText.text = "";
        gameObject.SetActive(true);
    }
    
    void OnDestroy()
    {
        Keys.WinEvent -= EnableEndMenu;
        EnemyKillScript.LooseEvent -= EnableEndMenu;
    }
}
