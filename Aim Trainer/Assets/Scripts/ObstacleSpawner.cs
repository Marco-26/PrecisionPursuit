using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3 position;
    [SerializeField] private GameObject obstacle;
    private Vector3 lastObstaclePos = Vector3.zero;
    private Vector3 currObstaclePos = Vector3.zero;

    public void SpawnObstacle() {
        lastObstaclePos = currObstaclePos;
        currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), Random.Range(6, 8));

        while (lastObstaclePos == currObstaclePos) {
            currObstaclePos = new Vector3(Random.Range(-3, 3), Random.Range(6, 8), Random.Range(6, 8));
        }
       
        Instantiate(obstacle, currObstaclePos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, size);
    }
}
