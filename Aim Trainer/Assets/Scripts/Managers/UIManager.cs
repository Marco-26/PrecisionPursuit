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
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI accuracyTextGameOver;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    
    [SerializeField] private bool aditionalOptions;

    private Timer timer;

    private void Start() {
        gameOverScreen.SetActive(false);
        timer = GetComponent<Timer>();

        GameManager.Instance.OnGameEnd += GameManager_OnGameEnd;
        HandleButtonListeners();
    }
    
    private void Update() {
        HandleUI();
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
    }

    private void GameManager_OnGameEnd(object sender, EventArgs e) {
        gameOverScreen.SetActive(true);
        scoreTextGameOver.text = "Score: " + GameManager.Instance.GetScore().ToString();
        accuracyTextGameOver.text = "Accuracy: " + GameManager.Instance.GetAccuracy().ToString()+"%";
        GameManager.Instance.PauseGame();
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
