using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Grid{

    private Transform[,] gridArray;
    private int width;
    private int height;

    public Grid() {
        width = 3; height = 3;
        gridArray = new Transform[width, height];
    }

    public void SetValue(int x, int y, Transform target) {
        gridArray[x, y] = target;
    }

    public void ClearValue(int x, int y) {
        gridArray[x, y] = null;
    }

    public void PrintGrid() {
        Debug.Log("----------------------------------------------------------------------------");

        for (int i = 0;  i < gridArray.GetLength(0); i++) {
            for(int j = 0;  j < gridArray.GetLength(1); j++) {
                Debug.Log("Posicao x: " + i + ", Posicao y: " + j + ", Ocupado: " + gridArray[i,j]);
            }
        }
        Debug.Log("----------------------------------------------------------------------------");

    }

    public Transform GetValue(int x,int y) {
        return gridArray[x, y];
    }

    public int GetWidth() { return width; }
    public int GetHeight() { return height;}
}
