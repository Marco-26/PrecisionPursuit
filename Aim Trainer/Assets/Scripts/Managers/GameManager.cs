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
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerGun playerGun;

    private Gamemode currentGamemode = Gamemode.MOTION;

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
    }

    private void Start(){
        if(playerGun != null)
        {
            playerGun.OnShotsFired += PlayerGun_OnShotsFired;
            playerGun.OnShotsHit += PlayerGun_OnShotsHit;
        }
    }

    private void PlayerGun_OnShotsFired(object sender, PlayerGun.MissFireEventArgs e){
        playerAccuracy = e.accuracy;
    }

    private void PlayerGun_OnShotsHit(object sender, PlayerGun.HitFireEventArgs e){
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
