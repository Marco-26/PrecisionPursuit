using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }  

    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RectTransform crosshair;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject recordBeatenMessage;
    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI accuracyTextGameOver;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;

    [Header("Additional Options UI")]
    [SerializeField] private bool aditionalOptions;
    [SerializeField] private GameObject additionalOptionsContainer;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI gamemodeText;

    [Header("Options UI")]
    [SerializeField]  private GameObject options;
    [SerializeField]  private Button optionsMenuResumeButton;
    [SerializeField]  private Button optionsMenuQuitButton;

    private Timer timer;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        options.SetActive(false);
        gameOverScreen.SetActive(false);
        recordBeatenMessage.SetActive(false);

        timer = GetComponent<Timer>();

        GameManager.Instance.OnGameEnd += GameManager_OnGameEnd;
        
        if (!aditionalOptions) {
            additionalOptionsContainer.SetActive(false);
        } else {
            additionalOptionsContainer.SetActive(true);
            highscoreText.text = GameManager.Instance.GetPlayerHighscore().ToString();
            gamemodeText.text = GameManager.Instance.GetCurrentGamemode().ToString();
        }
        
        HandleButtonListeners();
    }
    
    private void Update() {
        HandleUI();
    }

    public void ChangeCrosshairUI(Sprite newCrosshair, int width, int height) {
        crosshair.sizeDelta = new Vector2 (width, height);
        crosshair.GetComponent<Image>().sprite = newCrosshair;
    }

    public void ShowOptionsMenu() {
        options.SetActive(true);
    }

    public void HideOptionsMenu() {
        options.SetActive(false);
    }

    private void DisplayTime() {
        float timeRemaining = timer.GetTimeRemaining();
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void DisplayAccuracy() {
        accuracyText.text = GameManager.Instance.GetAccuracy().ToString();
    }

    private void DisplayPoints() {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }

    private void HandleUI() {
        DisplayAccuracy();
        DisplayPoints();
        DisplayTime();
    }

    private void HandleButtonListeners() {
        restartButton.onClick.AddListener(() => {
            gameOverScreen.SetActive(false);
            GameManager.Instance.RestartGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            gameOverScreen.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        });

        optionsMenuResumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        optionsMenuQuitButton.onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });
    }

    private void GameManager_OnGameEnd(object sender, EventArgs e) {
        gameOverScreen.SetActive(true);
        if (GameManager.Instance.isHighscoreBeaten()) {
            recordBeatenMessage.SetActive(true);
        }
        scoreTextGameOver.text = "Score: " + GameManager.Instance.GetScore().ToString();
        accuracyTextGameOver.text = "Accuracy: " + GameManager.Instance.GetAccuracy().ToString()+"%";
    }

    
    public static void Quit() {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
    #else
                 Application.Quit();
    #endif
    }

}
