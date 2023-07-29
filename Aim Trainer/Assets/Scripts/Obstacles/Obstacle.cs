using UnityEngine;

public class Obstacle : MonoBehaviour {

    public void Destroy() {
        ObstacleSpawner.Instance.SpawnObstacle();
        
        Destroy(gameObject);
    }

}