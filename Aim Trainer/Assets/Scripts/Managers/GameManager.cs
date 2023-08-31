using System.ComponentModel;
using System;
using Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using UnityEngine.Serialization;

public enum Gamemode {
    GRIDSHOT,
    MOTIONSHOT,
    NULL
}

public enum GamemodeScenes {
    Gridshot,
    Motionshot,
    MainMenu
}

public enum CrosshairType {
    CROSSHAIR_LARGE,
    CROSSHAIR_MEDIUM,
    CROSSHAIR_SMALL
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerGun playerGun;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private Timer timer;
    [SerializeField] private CountdownTimer countdownTimer;

    private Gamemode currentGamemode = Gamemode.NULL;

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

        playerManager.OnPauseKeyPressed += PlayerGun_OnPauseKeyPressed;
    }

    private void PlayerGun_OnPauseKeyPressed(object sender, EventArgs e) {
        TogglePauseGame();
    }

    private void Timer_OnTimerEnd(object sender, EventArgs e) {
        SaveManager.Instance.SaveData();
        PlayerManager.Instance.enabled = false;
        PauseGame();
        OnGameEnd?.Invoke(this, EventArgs.Empty);
    }

    private void CountdownTimer_OnTimerEnd(object sender, EventArgs e) {
        isGameStarted = true;
    }

    public Gamemode GetCurrentGamemode() { return currentGamemode; }

    public float GetPlayerHighscore() { return playerGun.GetHighscore(); }

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
        return playerGun.IsHighscoreBeaten();
    }
}
