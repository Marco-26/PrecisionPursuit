using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject additionalOptionsContainer;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI gamemodeText;
    
    private Timer timer;
    [SerializeField] private bool aditionalOptions;

    private void Start() {
        timer = GetComponent<Timer>();

        if (!aditionalOptions) {
            additionalOptionsContainer.SetActive(false);
        } else {
            additionalOptionsContainer.SetActive(true);
            highscoreText.text = GameManager.Instance.GetPlayerHighscore().ToString();
            gamemodeText.text = GameManager.Instance.GetCurrentGamemode().ToString();
        }
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
