using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public enum Gamemode {
    FLICKING,
    MOTION
}

public class GameManager : MonoBehaviour {
    //TODO: fazer uma class apenas para o UI, para nao ser o gameManager a tratar de UI
    public static GameManager instance { get; private set; }

    private bool timerIsRunning = false;

    [SerializeField] private PlayerGun playerGun;

    private Gamemode currentGamemode;

    public int totalTargets { get; private set; } = 20;
    public int totalShotsFired { get; private set; } = 0;
    public int totalShotsHit { get; private set; } = 0;

    private const int BASESCORE = 10;
    private float score = 0;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        if(playerGun != null)
        {
            playerGun.OnShotsFired += PlayerGun_OnShotsFired;
            playerGun.OnShotsHit += PlayerGun_OnShotsHit;
        }
    }

    private void PlayerGun_OnShotsFired(object sender, PlayerGun.ShotsFiredEventArgs e){
        this.totalShotsFired = e.totalShotsFired;
    }

    private void PlayerGun_OnShotsHit(object sender, PlayerGun.ShotsHitEventArgs e){
        this.totalShotsHit = e.totalShotsHit;
        score += (BASESCORE * calculateAccuracy())/2;

    }

    public float calculateAccuracy(){
        if (totalShotsFired <= 0){
            return 0;
        }
        return ((float)totalShotsHit / totalShotsFired) * 100;
    }

    public float calculateScore() {
        return score;
    }

    public void SetTimerIsRunning(bool option) {
        timerIsRunning = option;
    }

    public bool isTimerRunning() {
        return timerIsRunning;
    }

    public void SetCurrentGamemode(Gamemode gamemode) {
        currentGamemode = gamemode;
    }
}
