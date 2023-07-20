using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class GameManager : MonoBehaviour {
    //TODO: fazer uma class apenas para o UI, para nao ser o gameManager a tratar de UI
    public static GameManager instance { get; private set; }

    [HideInInspector] public bool timerIsRunning = false;

    [SerializeField] private PlayerGun playerGun;

    public int totalTargets { get; private set; } = 20;
    public int totalShotsFired { get; private set; } = 0;
    public int totalShotsHit { get; private set; } = 0;

    private const int BASESCORE = 10;
    private float score = 0;

    public enum Gamemode {
        TIMER_BASED,
        TARGET_BASED,
        UNSELECTED
    }

    public Gamemode currentGamemode { get; set; } = Gamemode.TIMER_BASED;


    void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start(){
        Debug.Log(totalTargets);
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
}
