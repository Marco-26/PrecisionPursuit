using System.ComponentModel;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public enum Gamemode {
    FLICKING,
    TRACKING,
    NULL
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerGun playerGun;
    [SerializeField] private GameModeSettings gamemodeSettings;
    [SerializeField] private Timer timer;

    private Gamemode currentGamemode = Gamemode.FLICKING;

    private float playerAccuracy = 0;
    private float playerScore = 0;
    private float playerHighscore = 0;

    private bool gameEnded = false;


    private void Awake() {
        Instance = this;
        currentGamemode = gamemodeSettings.chosenGamemode;
    }

    private void Start() {
        SaveManager.Instance.Load(currentGamemode);
        playerHighscore = SaveManager.Instance.GetSavedHighscore();

        timer.OnTimerEnd += Timer_OnTimerEnd;

        if (playerGun != null)
        {
            if(currentGamemode == Gamemode.FLICKING) {
                playerGun.OnShotsFired += PlayerGun_OnShotsFired;
                return;
            }
            playerGun.OnTrackedObstacle += PlayerGun_OnTrackedObstacle;
        }
    }

    private void PlayerGun_OnTrackedObstacle(object sender, PlayerGun.FireEventArgs e) {
        playerScore = e.score;
        playerAccuracy = e.accuracy;
    }

    private void PlayerGun_OnShotsFired(object sender, PlayerGun.FireEventArgs e){
        playerScore = e.score;
        playerAccuracy = e.accuracy;
    }

    private void Timer_OnTimerEnd(object sender, EventArgs e) {
        gameEnded = true;
        if(playerScore > playerHighscore) {
            SaveManager.Instance.Save(currentGamemode, playerScore);
        }
        Debug.Log("New Highscore: " + SaveManager.Instance.GetSavedHighscore());
    }

    public int GetAccuracy() {
        return Mathf.FloorToInt(playerAccuracy);
    }

    public int GetScore() {
        return Mathf.FloorToInt(playerScore);
    }

    public Gamemode GetCurrentGamemode() { return currentGamemode; }

    public float GetPlayerHighscore() { return  playerHighscore; }

    public void SetCurrentGamemode(Gamemode gamemode) {
        currentGamemode = gamemode;
    }
}
