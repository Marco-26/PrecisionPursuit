using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int obstaclesDestroyed; //keep track of how many obstacles player has destroyed

    public int maxKillCount = 5;
    public bool timerIsRunning = false;

    [SerializeField] private float timeRemaining;
    [SerializeField] private Text info;
 

    void Start()
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

    void SelectGamemode() {
        GameValues.Gamemode gamemode = GetGamemode();
        if(gamemode == GameValues.Gamemode.Unselected) {
            return;
        }

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
