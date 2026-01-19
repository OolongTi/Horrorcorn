using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinOrLoseText;
    private static string PreviousEndMessage = "";

    public static StartMenu instance;

    public static event Action MenuOpen;
    public static event Action MenuClosed;
    
    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Keys.WinEvent += EnableEndMenu;
        EnemyKillScript.LooseEvent += EnableEndMenu;
        VoidScript.FellIntoVoid += FellIntoVoid;
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
    
    void FellIntoVoid()
    {
        EnableEndMenu("You fell into the void!");
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
        MenuClosed?.Invoke();
        PlayerCamera.LockCursor();
        gameObject.SetActive(false);
    }
    
    public void QuitClicked()
    {
        Application.Quit();
    }
    
    private void EnableEndMenu(string message)
    {
        MenuOpen?.Invoke();
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
