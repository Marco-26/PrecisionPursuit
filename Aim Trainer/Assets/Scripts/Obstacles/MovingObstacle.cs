using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private ObstacleSpawner obstacleSpawner;
    private int moveSpeed = 1;
    private Transform myTransform;    

    private void Start() {
        myTransform = GetComponent<Transform>();
        obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);
        int randomNumber = Random.Range(0, 100);

        inputVector.x += 1;

        if (randomNumber < 10) {
            inputVector.x -= 1;
        }

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public void Destroy() {
        obstacleSpawner.SpawnObstacle();

        Destroy(gameObject);
    }

}