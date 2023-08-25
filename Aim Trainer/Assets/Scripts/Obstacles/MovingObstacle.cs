using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObstacle : MonoBehaviour, IDestroyable {
    
    private Transform myTransform;
    private MovingObstacleDestroyTimer destroyTimer;

    private int moveSpeed;
    private int positionY,positionX;
    private float reverseProbability = 0.5f;
    
    private void Start()
    {
        moveSpeed = Random.Range(2, 5);
        if (Random.value < reverseProbability)
        {
            moveSpeed *= -1;
        }
        
        myTransform = GetComponent<Transform>();
        destroyTimer = GetComponent <MovingObstacleDestroyTimer>();

        positionX = Mathf.RoundToInt(transform.position.x);
        positionY = Mathf.RoundToInt(transform.position.y);

        destroyTimer.OnDestroy += DestroyTimer_OnDestroy;
    }

    private void DestroyTimer_OnDestroy(object sender, System.EventArgs e) {
        Destroy();
    }

    private void OnTriggerEnter(Collider other) {
        Destroy();
    }

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector.x += 1;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * (moveSpeed * Time.deltaTime);
    }

    private void ReverseDirection() {
        moveSpeed *= -1;
    }

    public void Destroy() {
        GridManager.Instance.SpawnObstacle();
        GridManager.Instance.RemoveFromGrid(positionX, positionY);
        Destroy(gameObject);
    }

}