using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingObstacleDestroyTimer : MonoBehaviour{
    private const int TIMER_MIN = 3;
    private const int TIMER_MAX = 5;
    private float timer;

    public event EventHandler OnDestroy;

    private void Start() {
        timer = Random.Range(TIMER_MIN, TIMER_MAX);
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) { 
            timer = Random.Range(TIMER_MIN, TIMER_MAX);
            OnDestroy?.Invoke(this, EventArgs.Empty);
        }
    }
}
