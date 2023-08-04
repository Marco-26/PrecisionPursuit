using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingObstacleDestroyTimer : MonoBehaviour{
    private const int NOTIFY_TIME_AMOUNT = 2;
    private float timer;
    private MovingObstacle movingObstacle;

    public event EventHandler OnDestroy;


    private void Start() {
        movingObstacle = GetComponent<movingObstacle>();
    }

    private void Update() {
        if (!movingObstacle.GetIsBeingTracked()) {
            return;
        }

        timer -= Time.deltaTime;
        if (timer <= 0) { 
            timer = NOTIFY_TIME_AMOUNT;
            //TODO: MANDAR ESTE EVENTO PARA UMA NOVA CLASSE OBSTACLE HEALTH, E A PARTIR DE LA DESTRUIR
            //OnDestroy?.Invoke(this, EventArgs.Empty);
        }
    }
}
