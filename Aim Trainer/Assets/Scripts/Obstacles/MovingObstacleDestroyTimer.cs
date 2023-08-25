using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MovingObstacleDestroyTimer : MonoBehaviour{
    private const int TIMER_MIN = 2;
    private const int TIMER_MAX = 4;
    private float timer;

    public event EventHandler OnDestroy;

    private void Start() {
        timer = Random.Range(TIMER_MIN, TIMER_MAX);
    }

    private void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0) { 
            OnDestroy?.Invoke(this, EventArgs.Empty);
        }
    }
}
