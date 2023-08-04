using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MovingObstacle : MonoBehaviour {
    
    private int moveSpeed;
    private Transform myTransform;
    private MovingObstacleDestroyTimer destroyTimer;
    private Renderer myRenderer;
    private bool isBeingTracked = false;

    private Color trackedColor = Color.blue;
    private Color defaultColor = Color.red;

    private void Start() {
        moveSpeed = Random.Range(2, 5);
        myRenderer = GetComponent<Renderer>();
        myTransform = GetComponent<Transform>();
        destroyTimer = GetComponent <MovingObstacleDestroyTimer>();

        destroyTimer.OnDestroy += DestroyTimer_OnDestroy;
    }

    private void DestroyTimer_OnDestroy(object sender, System.EventArgs e) {
        Destroy(gameObject);
        ObstacleSpawner.Instance.SpawnObstacle();
    }

    private void OnTriggerEnter(Collider other) {
        ReverseDirection();
    }

    private void Update() {
        Debug.Log(isBeingTracked);
        Vector2 inputVector = new Vector2(0, 0);

        inputVector.x += 1;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    private void ChangeColorWhenTracked() {
        myRenderer.material.SetColor("_Color", trackedColor);
    }

    private void ResetColor() {
        myRenderer.material.SetColor("_Color", defaultColor);
    }

    private void ReverseDirection() {
        moveSpeed *= -1;
    }

    public void SetIsBeingTracked() {
        ChangeColorWhenTracked();
        isBeingTracked = true;
    }

    public void SetIsNotBeingTracked() {
        ResetColor();
        isBeingTracked = false;
    }

    public bool GetIsBeingTracked() {
        return isBeingTracked;
    }
}