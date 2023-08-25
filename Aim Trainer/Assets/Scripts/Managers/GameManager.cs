using System.ComponentModel;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public enum Gamemode {
    GRIDSHOT,
    MOTIONSHOT,
    NULL
}

public enum GamemodeScenes {
    Gridshot,
    Motionshot
}

public enum CrosshairType {
    CROSSHAIR_LARGE,
    CROSSHAIR_MEDIUM,
    CROSSHAIR_SMALL
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerGun playerGun;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private Timer timer;
    [SerializeField] private CountdownTimer countdownTimer;

    private Gamemode currentGamemode = Gamemode.NULL;

    private float playerAccuracy = 0;
    private float playerScore = 0;

    private bool isGamePaused = false;
    private bool isGameStarted = false;

    public event EventHandler OnGameEnd;

    private void Awake() {
        Instance = this;

        string gamemodeSaved = PlayerPrefs.GetString("gamemode");
        if(!Enum.TryParse(gamemodeSaved, out currentGamemode)) {
            Debug.LogError("Error parsing gamemode string into enum!");
        }
    }

    private void Start() {
        UnpauseGame();

        timer.OnTimerEnd += Timer_OnTimerEnd;
        countdownTimer.OnCountdownTimerStopped += CountdownTimer_OnTimerEnd;

        playerGun.OnShotsFired += PlayerGun_OnShotsFired;
        playerInput.OnPauseKeyPressed += PlayerGun_OnPauseKeyPressed;
    }

    private void PlayerGun_OnPauseKeyPressed(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void PlayerGun_OnShotsFired(object sender, PlayerGun.FireEventArgs e){
        playerScore = e.score;
        playerAccuracy = e.accuracy;
    }

    private void Timer_OnTimerEnd(object sender, EventArgs e) {
        if (IsHighscoreBeaten()) {
            SaveManager.Instance.SaveSensibleData();
        }
        PauseGame();
        OnGameEnd?.Invoke(this, EventArgs.Empty);
    }

    private void CountdownTimer_OnTimerEnd(object sender, EventArgs e) {
        isGameStarted = true;
    }

    public int GetAccuracy() {
        return Mathf.FloorToInt(playerAccuracy);
    }

    public int GetScore() {
        return Mathf.FloorToInt(playerScore);
    }

    public Gamemode GetCurrentGamemode() { return currentGamemode; }

    public float GetPlayerHighscore() { return playerGun.GetHighscore(); }

    public void SetCurrentGamemode(Gamemode gamemode) {
        currentGamemode = gamemode;
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if (isGamePaused) {
            OptionsUI.Instance.ShowOptionsMenu();
            PauseGame();
        } else {
            OptionsUI.Instance.HideOptionsMenu();
            UnpauseGame();
        }
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerLook.enabled = true;

        if (!isGameStarted) {
            return;
        }
        
        playerGun.enabled = true;
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public PlayerGun GetPlayerGun() {
        return playerGun;
    }

    public bool IsHighscoreBeaten() {
        return playerScore > GetPlayerHighscore();
    }
}
