using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UIManager : MonoBehaviour
{
    private float timeRemaining = 60;
    [SerializeField] private Text timerText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI pointsText;
    
    private void Update() {
        HandleUI();
    }

    private void DisplayTime(float timeRemaining) {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void DisplayAccuracy() {
        accuracyText.text = Mathf.FloorToInt(GameManager.instance.calculateAccuracy()) + "%";
    }

    private void DisplayPoints() {
        pointsText.text = Mathf.FloorToInt(GameManager.instance.calculateScore()).ToString();
    }

    private void UpdateTimer() {
        DisplayTime(timeRemaining);
        if (GameManager.instance.isTimerRunning()) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                timeRemaining = 0;
                GameManager.instance.SetTimerIsRunning(false);
                Quit();
            }
        }
    }

    private void HandleUI() {
        DisplayAccuracy();
        DisplayPoints();
        UpdateTimer();
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
