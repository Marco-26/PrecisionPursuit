using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector3 position;
    [SerializeField] private GameObject obstacle;

    public void SpawnObstacle() {
        var obstacle_position = new Vector3(Random.Range(-3,3), Random.Range(6, 8), Random.Range(6,8));
        Instantiate(obstacle, obstacle_position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, size);
    }
}
