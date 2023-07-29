using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerGun : MonoBehaviour {
    private const int BASE_SCORE = 10;

    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;
    
    private int totalShotsFired = 0;
    private int totalShotsHit = 0;
    private float timeTrackingObstacle = 0f;
    private float timeNotTrackingObstacle = 0f;
    private float score = 0;

    private Transform obstacle = null;

    public event EventHandler<FireEventArgs> OnShotsFired;
    public event EventHandler<FireEventArgs> OnTrackedObstacle;
        

    public class FireEventArgs : EventArgs{
        public float score;
        public float accuracy;
    }

    private void Update(){
        if(GameManager.Instance.GetCurrentGamemode() == Gamemode.TRACKING) {
            Track();
            return;
        }

        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        totalShotsFired++;
        float tempScore = score;
        OnShotsFired?.Invoke(this, new FireEventArgs() { score = CalculateScore(false), accuracy = CalculateAccuracy() }) ;

        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                totalShotsHit++;
                OnShotsFired?.Invoke(this, new FireEventArgs() { score = CalculateScore(true), accuracy = CalculateAccuracy() });
            }
        }
    }

    private void Track() {
        if (obstacle != null) {
            MovingObstacle target = obstacle.GetComponent<MovingObstacle>();
            if (target != null) {
                target.ResetColor();
            }
            obstacle = null;
        }

        if (Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            obstacle = hit.transform;
            if (obstacle != null) {
                MovingObstacle target = obstacle.GetComponent<MovingObstacle>();
                if (target != null) {
                    timeTrackingObstacle += Time.deltaTime;
                    target.ChangeColorWhenTracked();
                    OnTrackedObstacle?.Invoke(this, new FireEventArgs { accuracy = CalculateAccuracy(), score = CalculateScore(true) });
                    return;
                }
                timeNotTrackingObstacle += Time.deltaTime;
                OnTrackedObstacle?.Invoke(this, new FireEventArgs { accuracy = CalculateAccuracy(), score = CalculateScore(false) });

            }
        }
    }

    private float CalculateAccuracy() {
        switch (GameManager.Instance.GetCurrentGamemode()) {
            case Gamemode.FLICKING:
                if (totalShotsFired <= 0) {
                    return 0;
                }
                return ((float)totalShotsHit / totalShotsFired) * 100;
            case Gamemode.TRACKING:
                float totalTime = timeTrackingObstacle + timeNotTrackingObstacle;
                if(totalTime <=0) {
                    return 0;
                }

                return (timeTrackingObstacle / totalTime) * 100;
        }
        return 0;
    }

    private float CalculateScore(bool hit) {
        if (hit) {
            if(GameManager.Instance.GetCurrentGamemode() == Gamemode.FLICKING) {
                score += (BASE_SCORE * CalculateAccuracy()) / 2;
                return score;
            }

            score += (CalculateAccuracy()) / 15;
            return score;
        } else {
            if (score <= 0) {
                return 0;
            }

            if (GameManager.Instance.GetCurrentGamemode() == Gamemode.FLICKING) {
                score -= (BASE_SCORE * CalculateAccuracy()) / 4;
                return score;
            }

            score -= (CalculateAccuracy()) / 40;
            return score;
        }
    }
}
