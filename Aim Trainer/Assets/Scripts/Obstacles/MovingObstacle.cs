using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObstacle : MonoBehaviour, IDestroyable {
    
    private int moveSpeed;
    private Transform myTransform;
    private MovingObstacleDestroyTimer destroyTimer;

    int y,x;

    private void Start() {
        //moveSpeed = Random.Range(2, 5);
        moveSpeed = 1;
        myTransform = GetComponent<Transform>();
        destroyTimer = GetComponent <MovingObstacleDestroyTimer>();

        x = Mathf.RoundToInt(transform.position.x);
        y = Mathf.RoundToInt(transform.position.y);

        destroyTimer.OnDestroy += DestroyTimer_OnDestroy;
    }

    private void DestroyTimer_OnDestroy(object sender, System.EventArgs e) {
        Destroy();
    }

    private void OnTriggerEnter(Collider other) {
        ReverseDirection();
    }

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector.x += 1;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void ReverseDirection() {
        moveSpeed *= -1;
    }

    public void Destroy() {
        GridManager.Instance.SpawnObstacle();
        GridManager.Instance.RemoveFromGrid(x, y);
        Destroy(gameObject);
    }

}