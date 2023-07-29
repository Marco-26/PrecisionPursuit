using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class MovingObstacleDirectionTimer : MonoBehaviour{
    
    private float time = 0;
    private const int MIN_TIME = 1;
    private const int MAX_TIME = 5;

    public event EventHandler OnTimeEnd;

    private void Start() {
        time = Random.Range(MIN_TIME, MAX_TIME);
    }

    private void Update() {
        time -= Time.deltaTime;
        if(time < 0) {
            time = Random.Range(MIN_TIME, MAX_TIME);
            OnTimeEnd?.Invoke(this, EventArgs.Empty);
        }
    }
}
