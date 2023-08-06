using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Timer : MonoBehaviour{

    private float timeRemaining = 60;
    private bool isRunning = false;

   private CountdownTimer countdownTimer;
    
    private void Start() {
        countdownTimer = GetComponent<CountdownTimer>();
        if(countdownTimer != null) {
            countdownTimer.OnCountdownTimerStopped += Timer_OnCountdownTimerStopped;
        }
    }

    private void Timer_OnCountdownTimerStopped(object sender, EventArgs e) {
        isRunning = true;
    }

    private void Update() {
        if (isRunning) {
            UpdateTimer();
        }
    }

    private void UpdateTimer() {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0) {
            timeRemaining = 0;
            isRunning = false;
        }
    }

    public float GetTimeRemaining() {
        return timeRemaining;
    }

}
