using System.ComponentModel;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private GameSettignsSO gameSettings;
    [SerializeField] private CountdownTimer countdownTimer;
    [SerializeField] private Timer timer;

    private Gamemode currentGamemode = Gamemode.FLICKING;

    private float playerAccuracy = 0;
    private float playerScore = 0;
    private float playerHighscore = 0;

    private bool gameEnded = false;

    public event EventHandler OnGameEnd;

    private void Awake() {
        Instance = this;
        currentGamemode = gameSettings.chosenGamemode;
        
        SaveManager.Load(currentGamemode, out playerHighscore);
        Debug.Log(playerHighscore);
    }

    private void Start() {
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
        if(isHighscoreBeaten()) {
            SaveManager.Save(currentGamemode, playerScore);
        }
        PauseGame();
        OnGameEnd?.Invoke(this, EventArgs.Empty);
    }

    public int GetAccuracy() {
        return Mathf.FloorToInt(playerAccuracy);
    }

    public int GetScore() {
        return Mathf.FloorToInt(playerScore);
    }

    public Gamemode GetCurrentGamemode() { return currentGamemode; }

    public float GetPlayerHighscore() { return playerHighscore; }

    public void SetCurrentGamemode(Gamemode gamemode) {
        currentGamemode = gamemode;
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        playerGun.enabled = false;
        playerLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UnpauseGame() {
        Time.timeScale = 1f;
        playerGun.enabled = true;
        playerLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public PlayerGun GetPlayerGun() {
        return playerGun;
    }

    public bool isHighscoreBeaten() {
        return playerScore > playerHighscore;
    }
}
