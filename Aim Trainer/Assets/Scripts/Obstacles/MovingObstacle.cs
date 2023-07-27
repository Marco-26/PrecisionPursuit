using UnityEngine;

public class MovingObstacle : MonoBehaviour {

    private int moveSpeed = 1;
    private Transform myTransform;
    private MovingObstacleTimer timer;
    private Renderer renderer;

    private void Start() {
        renderer = GetComponent<Renderer>();
        myTransform = GetComponent<Transform>();
        
        timer = GetComponent<MovingObstacleTimer>();
        if (timer != null) {
            timer.OnTimeEnd += Timer_OnTimeEnd;
        }
    }

    private void Timer_OnTimeEnd(object sender, System.EventArgs e) {
        moveSpeed *= -1;
    }

    private void Update() {
        Vector2 inputVector = new Vector2(0, 0);

        inputVector.x += 1;

        Vector3 moveVector = new Vector3(inputVector.x, 0, inputVector.y);

        myTransform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public void ChangeColorWhenTracked() {
        renderer.material.SetColor("_Color", Color.blue);
    }

    public void ResetColor() {
        renderer.material.SetColor("_Color", Color.red);
    }
}