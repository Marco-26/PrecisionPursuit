using UnityEngine;

public class Obstacle : MonoBehaviour {

    private ObstacleSpawner obstacleSpawner;

    private void Start() {
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    public void Destroy() {
        obstacleSpawner.SpawnObstacle();
        
        Destroy(gameObject);
    }

}