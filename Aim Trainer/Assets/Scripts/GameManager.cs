using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class GameManager : MonoBehaviour
{
    //TODO: fazer uma class apenas para o UI, para nao ser o gameManager a tratar de UI
    public static GameManager instance { get; private set; }

    [HideInInspector] public int obstaclesDestroyed = 0; //keep track of how many obstacles player has destroyed
    [HideInInspector] public int totalShots = 0;
    [HideInInspector] public bool timerIsRunning = false;
    
    public int maxKillCount = 20;

    [SerializeField] private float timeRemaining;
    [SerializeField] private Text info;
    [SerializeField] private TextMeshProUGUI accText;

    void Awake()
    {
        if(instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {
        SelectGamemode();
    }

    private GameValues.Gamemode GetGamemode() {
        if (GameValues.currentGamemode == GameValues.Gamemode.TimerBased)
            return GameValues.Gamemode.TimerBased;
        if (GameValues.currentGamemode == GameValues.Gamemode.TargetBased)
            return GameValues.Gamemode.TargetBased;

        return GameValues.Gamemode.Unselected;
    }

    #region gamemodes
    void TimerGamemode() {
        DisplayTime(timeRemaining);
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
            }
            else {
                timeRemaining = 0;
                timerIsRunning = false;
                //TODO screen telling player game is over
                Quit();
            }
        }
    }

    void KillAmountGamemode() {
        info.text = "Obstacles destroyed: " + obstaclesDestroyed.ToString();
        if (obstaclesDestroyed >= maxKillCount) {
            //TODO screen telling player game is over
            Quit();
        }
    }
    #endregion

    // timer
    void DisplayTime(float timeRemaining) {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        info.text = string.Format("{0:00}:{1:00}", minutes, seconds);   
    }

    void DisplayAccuracy() {
        if(totalShots <= 0) { return; }
        float acc = ((float) obstaclesDestroyed / totalShots) * 100;
        accText.text = Mathf.FloorToInt(acc) + "%";
    }

    void SelectGamemode() {
        GameValues.Gamemode gamemode = GetGamemode();
        if(gamemode == GameValues.Gamemode.Unselected) {
            return;
        }
        DisplayAccuracy();
        switch (gamemode) {
            case GameValues.Gamemode.TimerBased:
                TimerGamemode();
                break;
            case GameValues.Gamemode.TargetBased:
                KillAmountGamemode();
                break;
        }
    }

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
