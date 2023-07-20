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
        HandleSelectedGameMode();
    }

    void DisplayTime(float timeRemaining) {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    void DisplayAccuracy() {
        if (GameManager.instance.totalShots <= 0) { return; }
        accuracyText.text = Mathf.FloorToInt(GameManager.instance.calculateAccuracy()) + "%";
    }

    void DisplayPoints() {
        pointsText.text = Mathf.FloorToInt(GameManager.instance.calculateScore()).ToString();
    }

    void UpdateTimerGameMode() {
        DisplayTime(timeRemaining);
        if (GameManager.instance.timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                timeRemaining = 0;
                GameManager.instance.timerIsRunning = false;
                //TODO screen telling player game is over
                Quit();
            }
        }
    }

    void UpdatTargetGameMode() {
        timerText.text = "Obstacles destroyed: " + GameManager.instance.obstaclesDestroyed.ToString();
        if (GameManager.instance.obstaclesDestroyed >= GameManager.instance.maxKillCount) {
            //TODO screen telling player game is over
            Quit();
        }
    }

    void HandleSelectedGameMode() {
        if (GameManager.instance.currentGamemode == Gamemode.UNSELECTED) {
            return;
        }

        DisplayAccuracy();
        DisplayPoints();

        switch (GameManager.instance.currentGamemode) {
            case Gamemode.TIMER_BASED:
                UpdateTimerGameMode();
                break;
            case Gamemode.TARGET_BASED:
                UpdatTargetGameMode();
                break;
        }
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
