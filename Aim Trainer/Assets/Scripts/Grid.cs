using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid{
    private struct ObstaclePositions {
        public int x, y;
        public Obstacle obstacle;
    }

    List<ObstaclePositions> obstacles;
    
    public Grid() {
        obstacles = new List<ObstaclePositions>();
        
        for (int i = -1; i < 2; i++) {
            for (int j = 4; j < 7; j++) {
                Debug.Log("x: " + i + ", y: " + j);
                Obstacle newObstacle = ObstacleSpawner.Instance.SpawnTest(i, j);

                ObstaclePositions obstaclePos = new ObstaclePositions {
                    x = i,
                    y = j,
                    obstacle = newObstacle
                };

                obstacles.Add(obstaclePos);
            }
        }

        Debug.Log(obstacles);
    }


}
