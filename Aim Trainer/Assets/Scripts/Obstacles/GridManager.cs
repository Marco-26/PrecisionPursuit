using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour {
    public static GridManager Instance { get; private set; }

    [SerializeField] private Transform obstacle;
    [SerializeField] private CountdownTimer countdownTimer;

    private GridMap grid;
    private Vector3Int spawnPos = Vector3Int.zero;

    private void Awake() {
        Instance = this;
        grid = GetComponent<GridMap>();

        countdownTimer.OnCountdownTimerStopped += CountdownTimer_OnCountdownTimerStopped;        
    }

    private void CountdownTimer_OnCountdownTimerStopped(object sender, System.EventArgs e) {
        if(GameManager.Instance.GetCurrentGamemode() == Gamemode.GRIDSHOT) {
            Debug.Log("Aqui");
            for(int i = 0; i < 3; i++) {
                SpawnObstacle();
            }
            return;
        }

        SpawnObstacle();
    }

    public void SpawnObstacle() {
        Vector3 lastSpawnPos = spawnPos;
        spawnPos.z = GridMap.Z_DISTANCE;
        spawnPos.x = Random.Range(0, grid.GetWidth());
        spawnPos.y = Random.Range(grid.GetOffset().y, grid.GetHeight());

        Transform temp = grid.GetValue(spawnPos.x, spawnPos.y);

        while (temp != null || lastSpawnPos.y == spawnPos.y) {
            spawnPos.x = Random.Range(0, grid.GetWidth());
            spawnPos.y = Random.Range(grid.GetOffset().y, grid.GetHeight());
            temp = grid.GetValue(spawnPos.x, spawnPos.y);
        }

        //Depending on the scene, the obstacle can be static or not
        grid.SetValue(spawnPos.x, spawnPos.y, obstacle);
        Instantiate(obstacle, spawnPos, Quaternion.identity);
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

