using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int obstacleKillCount; //keep track of how many obstacles player has destroyed

    private int maxKillCount = GameValues.maxKillCount;
    private float timeRemaining = 120f;
    private bool timerIsRunning = false;

    private float startingTime = 3f;

    [SerializeField] private Text info;
    [SerializeField] private Text countdownText;

    // these elements are dependent of can play variable
    [SerializeField] private GameObject[] objects;


    #region Gamemode
    public enum Gamemode {
        Timer, 
        KillAmount
    }

    public Gamemode gamemode;
    #endregion

    void Start()
    {
        GameValues.canPlay = false;
        Application.targetFrameRate = 60;
        if(instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        SelectGamemode();
        timerIsRunning = true;
    }

    private void Update() {
        Debug.Log(GameValues.canPlay);
        Countdown();
        SelectGamemode();
    }

    void GetGamemode() {
        if (GameValues.GamemodeSTR == "Timer")
            gamemode = Gamemode.Timer;
        if (GameValues.GamemodeSTR == "KillAmount")
            gamemode = Gamemode.KillAmount;
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
        info.text = "Obstacles destroyed: " + obstacleKillCount.ToString();
        if (obstacleKillCount >= maxKillCount) {
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

    void SelectGamemode() {
        GetGamemode();
        switch (gamemode) {
            case Gamemode.Timer:
                TimerGamemode();
                break;
            case Gamemode.KillAmount:
                KillAmountGamemode();
                break;
        }
    }

    private void Countdown()
    {
        startingTime -= 1 * Time.deltaTime;
        if(startingTime <= 0)
        {
            countdownText.enabled = false;
            GameValues.canPlay = true; 
        }
        countdownText.text = startingTime.ToString("0");
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
