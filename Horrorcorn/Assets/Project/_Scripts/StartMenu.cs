using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI WinOrLoseText;
    private static string PreviousEndMessage = "";

    [SerializeField] private TimeManager timeManager;
    [SerializeField] private TMPro.TMP_InputField nameField;
    [SerializeField] private TextMeshProUGUI highscoreDisplayField;

    public static StartMenu instance;
    public string PlayerName = "";

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        TimeManager.RemovePause(gameObject);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            WinOrLoseText = canvas.transform.Find("WinOrLoseText")?.GetComponent<TextMeshProUGUI>();
            highscoreDisplayField = canvas.transform.Find("HighscoreField")?.GetComponent<TextMeshProUGUI>();
            nameField = canvas.transform.Find("NameField")?.GetComponent<TMP_InputField>();
        }
    
        UpdateHighscoreDisplay();
    }

    public void StartClicked()
    {
        PlayerName = nameField.text;
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
        
        HighScores hs = new HighScores();
        Timer currentTimer = FindObjectOfType<Timer>();
        if (currentTimer != null && message == "You Won!")
        {
            hs.AddEntry(PlayerName, currentTimer.time);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        WinOrLoseText.text = "";
        gameObject.SetActive(true);
    }
    
    void OnDestroy()
    {
        Keys.WinEvent -= EnableEndMenu;
        EnemyKillScript.LooseEvent -= EnableEndMenu;
    }
    
    public void UpdateHighscoreDisplay()
    {
        HighScores hs = new HighScores();
        if (highscoreDisplayField != null)
            highscoreDisplayField.text = hs.ToString();
    }
}
