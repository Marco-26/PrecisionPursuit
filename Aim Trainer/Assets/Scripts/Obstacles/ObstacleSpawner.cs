using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;

    private Vector3 lastObstaclePos = Vector3.zero;
    private Vector3 currObstaclePos = Vector3.zero;
    private const int ZDISTANCE = 5;
    
    public void SpawnObstacle() {
        lastObstaclePos = currObstaclePos;
        currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), ZDISTANCE);

        while (lastObstaclePos == currObstaclePos) {
            currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), ZDISTANCE);
        }
       
        Instantiate(obstacle, currObstaclePos, Quaternion.identity);
    }
}
