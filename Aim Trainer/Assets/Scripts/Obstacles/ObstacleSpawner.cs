using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour{
    public static ObstacleSpawner Instance { get; private set; }

    [SerializeField] private Transform obstacle;
    [SerializeField] private Transform movingObstacle;
    [SerializeField] private CountdownTimer countdownTimer;

    private Vector3 lastObstaclePos = Vector3.zero;
    private Vector3 currObstaclePos = Vector3.zero;

    private const int ZDISTANCE = 3;
    private Grid grid;

    private void Awake() {
        Instance = this;
        
        countdownTimer.OnCountdownTimerStopped += CountdownTimer_OnCountdownTimerStopped;        
    }

    private void Start() {
        grid = new Grid();
    }

    private void CountdownTimer_OnCountdownTimerStopped(object sender, System.EventArgs e) {
        SpawnTest();
    }

    public void SpawnObstacle() {
        lastObstaclePos = currObstaclePos;
        currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), ZDISTANCE);

        while (lastObstaclePos == currObstaclePos) {
            currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), ZDISTANCE);
        }
        
        if(GameManager.Instance.GetCurrentGamemode() == Gamemode.FLICKING) { 
            Instantiate(obstacle, currObstaclePos, Quaternion.identity);
            return;
        }
        Instantiate(movingObstacle, currObstaclePos, Quaternion.identity);
    }

    public void SpawnTest() {
        int x = Random.Range(0, grid.GetWidth());
        int y = Random.Range(0, grid.GetHeight());

        Transform temp = grid.GetValue(x, y);
        
        while (temp != null) {
            x = Random.Range(0, grid.GetWidth());
            y = Random.Range(0, grid.GetHeight());
            temp = grid.GetValue(x, y);
        }

        grid.SetValue(x, y, obstacle);
        Instantiate(obstacle, new Vector3(x, y, ZDISTANCE), Quaternion.identity);
    }

    public void RemoveFromGrid(int x, int y) {
        grid.ClearValue(x, y);
    }

    public void PrintGridArray() {
        grid.PrintGrid();
    }
}

