using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Obstacle : MonoBehaviour, IDestroyable {

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
        GridManager.Instance.SpawnObstacle();
        GridManager.Instance.RemoveFromGrid(x, y);
        Destroy(gameObject);
    }
}