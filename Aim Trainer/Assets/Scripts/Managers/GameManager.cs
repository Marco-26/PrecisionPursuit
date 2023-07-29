using System.ComponentModel;
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

    private Gamemode currentGamemode = Gamemode.NULL;

    private float playerAccuracy = 0;
    private float playerScore = 0;


    private void Awake() {
        if (Instance != null) {
            Destroy(this);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        currentGamemode = gamemodeSettings.chosenGamemode;
    }

    private void Start(){
        if(playerGun != null)
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

    public int GetAccuracy() {
        return Mathf.FloorToInt(playerAccuracy);
    }

    public int GetScore() {
        return Mathf.FloorToInt(playerScore);
    }

    public Gamemode GetCurrentGamemode() { return currentGamemode; }

    public void SetCurrentGamemode(Gamemode gamemode) {
        currentGamemode = gamemode;
    }
}
