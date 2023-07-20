using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class GameManager : MonoBehaviour {
    //TODO: fazer uma class apenas para o UI, para nao ser o gameManager a tratar de UI
    public static GameManager instance { get; private set; }

    [HideInInspector] public int obstaclesDestroyed = 0; //keep track of how many obstacles player has destroyed
    public int totalShots { get; set; } = 0;
    [HideInInspector] public bool timerIsRunning = false;
    [HideInInspector] public int maxKillCount = 20;

    private int baseScore = 100;
    private float accuracy = 0;
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

    public float calculateAccuracy(){
        return ((float)GameManager.instance.obstaclesDestroyed / GameManager.instance.totalShots) * 100;
    }

    public float calculateScore() {
        return baseScore * accuracy;
    }
}
