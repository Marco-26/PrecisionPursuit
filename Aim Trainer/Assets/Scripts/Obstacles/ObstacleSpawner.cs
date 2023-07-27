using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private GameObject movingObstacle;
    [SerializeField] private CountdownTimer countdownTimer;

    private Vector3 lastObstaclePos = Vector3.zero;
    private Vector3 currObstaclePos = Vector3.zero;

    private const int ZDISTANCE = 5;

    private void Awake() {
        countdownTimer.OnCountdownTimerStopped += CountdownTimer_OnCountdownTimerStopped;        
    }

    private void CountdownTimer_OnCountdownTimerStopped(object sender, System.EventArgs e) {
        SpawnObstacle();
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
}
