using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Obstacle : MonoBehaviour {

    private Grid grid;
    private int x;
    private int y;
    private Transform transform;

    private void Start() {
        transform = GetComponent<Transform>();
        x = Mathf.RoundToInt(transform.position.x);
        y = Mathf.RoundToInt(transform.position.y);
    }

    public void SetGrid(Grid grid) {
        this.grid = grid;
    }

    public void Destroy() {
        ObstacleSpawner.Instance.SpawnTest();
        ObstacleSpawner.Instance.RemoveFromGrid(x, y);
        Destroy(gameObject);
    }

}