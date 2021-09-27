using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int obstacleKillCount;
    public int maxKillCount;

    #region timer
    [SerializeField] private float timeRemaining = 120f;
    [SerializeField] private Text timer;
    private bool timerIsRunning = false;
    #endregion

    #region Gamemode
    public enum Gamemode {
        Timer, 
        KillAmount
    }

    public Gamemode gamemode;
    #endregion

    void Start()
    {
        if(instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SelectGamemode();
        timerIsRunning = true;
    }
    private void Update() {
        SelectGamemode();
    }

    // timer
    void DisplayTime(float timeRemaining) {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    #region gamemodeStuff
    void TimerGamemode() {
        DisplayTime(timeRemaining);
        if (timerIsRunning) { 
            if(timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("Game over");
                Quit();
            }
        }
    }

    void KillAmountGamemode() {
        if(obstacleKillCount >= maxKillCount) {
            //End the game cause the player reached the max amount
            //TODO screen telling player game is over
            Quit();
        }
    }

    void GetGamemode() {
        if (GameValues.GamemodeSTR == "Timer") gamemode = Gamemode.Timer;
        if (GameValues.GamemodeSTR == "KillAmount") gamemode = Gamemode.KillAmount;
    }
    void SelectGamemode() {
        GetGamemode();
        switch (gamemode) {
            case Gamemode.Timer:
                TimerGamemode();
                break;
            case Gamemode.KillAmount:
                KillAmountGamemode();
                break;
        }
    }
    #endregion

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
