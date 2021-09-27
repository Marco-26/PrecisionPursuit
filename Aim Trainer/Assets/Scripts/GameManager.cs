using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int obstacleKillCount;

    void Start()
    {
        if(instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update() {
        EndGame();
    }

    void EndGame() {
        if(obstacleKillCount >= 30) {
            //End the game cause the player reached the max amount
            //TODO screen telling player game is over
            Debug.Log("Game over");
        }
    }

}
