using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour{

    private Transform[,] gridArray;
    [SerializeField] private int width = 4;
    [SerializeField] private int height = 6;
    [SerializeField] private Vector2Int offset = new Vector2Int(0,2);
    [SerializeField] private bool debug;
    
    public static int Z_DISTANCE = 5;

    private void Awake() {
        gridArray = new Transform[width, height];
    }

    public Transform GetValue(int x, int y) {
        return gridArray[x, y];
    }

    public void SetValue(int x, int y, Transform target) {
        gridArray[x, y] = target;
    }

    public void ClearValue(int x, int y) {
        gridArray[x, y] = null;
    }

    public void PrintGridArray() {
        for (int i = offset.x; i < gridArray.GetLength(0); i++) {
            for (int j = offset.y; j < gridArray.GetLength(1); j++) {
                Debug.Log("Posicao x: " + i + ", Posicao y: " + j + ", Ocupado: " + gridArray[i, j]);
            }
        }
    }

    private void OnDrawGizmos() {
        if (!debug) {
            return;
        }

        foreach (var point in GetWorldPositions()) {
            Gizmos.DrawWireCube(point, new Vector3(1, 1, 0));
        }
    }

    private IEnumerable<Vector3> GetWorldPositions() {
        for (int x = offset.x; x < width; x++) {
            for (int y = offset.y; y < height; y++) {
                yield return new Vector3(x, y,5 );
            }
        }
    }

    public int GetWidth() { return width; }
    public int GetHeight() { return height; }

    public Vector2Int GetOffset() { return offset; }
}
