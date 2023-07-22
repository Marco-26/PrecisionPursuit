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

    private void UpdateTimerGameMode() {
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

    private void UpdateTargetGameMode() {
        timerText.text = GameManager.instance.totalShotsHit.ToString() + "/" + GameData.instance.totalTargets;
        if (GameManager.instance.totalShotsHit >= GameManager.instance.totalTargets) {
            //TODO screen telling player game is over
            Quit();
        }
    }

    private void HandleSelectedGameMode() {
        if (GameData.instance.currentGamemode == Gamemode.UNSELECTED)
        {
            return;
        }

        DisplayAccuracy();
        DisplayPoints();

        switch (GameData.instance.currentGamemode)
        {
            case Gamemode.TIMER_BASED:
                UpdateTimerGameMode();
                break;
            case Gamemode.TARGET_BASED:
                UpdateTargetGameMode();
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
