using UnityEngine;

public class Obstacle : MonoBehaviour {

    ObstacleSpawner obstacleSpawner;

    private void Start() {
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    public void Destroy() {
        obstacleSpawner.SpawnObstacle();
        Destroy(gameObject);
    }
}