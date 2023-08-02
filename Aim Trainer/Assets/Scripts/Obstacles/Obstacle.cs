using UnityEngine;
using UnityEngine.PlayerLoop;

public class Obstacle : MonoBehaviour {

    private Grid grid;
    private int x;
    private int y;

    public void SetGrid(Grid grid) {
        this.grid = grid;
    }

    public void SetPosition(Vector2Int position) {
        Debug.Log(position);
        x = position.x;
        y = position.y;
    }

    public void Destroy() {
        Debug.Log(this);

        Debug.Log("Posicao x: " + x + ", Posicao y: " + y);

        ObstacleSpawner.Instance.SpawnTest();
        Destroy(gameObject);
    }

}