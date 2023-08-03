using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {
    
    private int moveSpeed;
    private Transform myTransform;
    private MovingObstacleDirectionTimer directionTimer;
    private MovingObstacleDestroyTimer destroyTimer;
    private Renderer myRenderer;

    private Color trackedColor = Color.blue;
    private Color defaultColor = Color.red;

    private void Start() {
        moveSpeed = Random.Range(1, 5);
        myRenderer = GetComponent<Renderer>();
        myTransform = GetComponent<Transform>();
        directionTimer = GetComponent<MovingObstacleDirectionTimer>();
        destroyTimer = GetComponent <MovingObstacleDestroyTimer>();

        directionTimer.OnTimeEnd += DirectionTimer_OnTimeEnd;
        destroyTimer.OnDestroy += DestroyTimer_OnDestroy;
    }

    private void DestroyTimer_OnDestroy(object sender, System.EventArgs e) {
        Destroy(gameObject);
        GridManager.Instance.SpawnObstacle();
    }

    private void OnTriggerEnter(Collider other) {
        ReverseDirection();
    }

    private void DirectionTimer_OnTimeEnd(object sender, System.EventArgs e) {
        ReverseDirection();
    }

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector.x += 1;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public void ChangeColorWhenTracked() {
        myRenderer.material.SetColor("_Color", trackedColor);
    }

    public void ResetColor() {
        myRenderer.material.SetColor("_Color", defaultColor);
    }

    private void ReverseDirection() {
        moveSpeed *= -1;
    }

}