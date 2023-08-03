using UnityEngine;

public class Grid{

    private Transform[,] gridArray;
    private int width;
    private int height;
    private Vector2Int offset;

    public Grid() {
        offset = new Vector2Int(0, 2);
        width = 4; height = 6;
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
        for (int i = offset.x;  i < gridArray.GetLength(0); i++) {
            for(int j = offset.y;  j < gridArray.GetLength(1); j++) {
                Debug.Log("Posicao x: " + i + ", Posicao y: " + j + ", Ocupado: " + gridArray[i,j]);
            }
        }
    }
    
    public int GetWidth() { return width; }
    public int GetHeight() { return height;}

    public Vector2Int GetOffset() { return offset; }
}
