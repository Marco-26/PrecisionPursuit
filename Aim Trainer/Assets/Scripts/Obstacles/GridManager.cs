using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager Instance { get; private set; }

    [SerializeField] private Transform obstacle;
    [SerializeField] private Transform movingObstacle;
    [SerializeField] private CountdownTimer countdownTimer;

    private const int MAX_TARGETS = 3;

    private GridMap grid;

    private void Awake() {
        Instance = this;
        grid = GetComponent<GridMap>();


        countdownTimer.OnCountdownTimerStopped += CountdownTimer_OnCountdownTimerStopped;        
    }

    private void CountdownTimer_OnCountdownTimerStopped(object sender, System.EventArgs e) {
        SpawnObstacle();
    }

    public void SpawnObstacle() {
        int x = Random.Range(0, grid.GetWidth());
        int y = Random.Range(grid.GetOffset().y, grid.GetHeight());

        Transform temp = grid.GetValue(x, y);
        
        while (temp != null) {
            x = Random.Range(0, grid.GetWidth());
            y = Random.Range(grid.GetOffset().y, grid.GetHeight());
            temp = grid.GetValue(x, y);
        }

        grid.SetValue(x, y, obstacle);

        if (GameManager.Instance.GetCurrentGamemode() == Gamemode.FLICKING) {
            Instantiate(obstacle, new Vector3(x,y, GridMap.Z_DISTANCE), Quaternion.identity);
            return;
        }

        Instantiate(movingObstacle, new Vector3(x, y, GridMap.Z_DISTANCE), Quaternion.identity);
    }

    public void RemoveFromGrid(int x, int y) {
        grid.ClearValue(x, y);
    }

    public void FillGrid() {
        for (int x = grid.GetOffset().x; x < grid.GetWidth(); x++) {
            for (int y = grid.GetOffset().y; y < grid.GetHeight(); y++) {
                grid.SetValue(x, y, obstacle);
                Instantiate(obstacle, new Vector3(x, y, GridMap.Z_DISTANCE), Quaternion.identity);
            }
        }
    }
}

