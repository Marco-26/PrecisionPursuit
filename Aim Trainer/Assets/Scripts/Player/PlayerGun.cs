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

    public event EventHandler<MissFireEventArgs> OnShotsFired;
    public event EventHandler<HitFireEventArgs> OnShotsHit;

    public class HitFireEventArgs : EventArgs{
        public float score;
        public float accuracy;
    }

    public class MissFireEventArgs : EventArgs {
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
        OnShotsFired?.Invoke(this, new MissFireEventArgs() { accuracy = CalculateAccuracy() }); ;

        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                totalShotsHit++;
                OnShotsHit?.Invoke(this, new HitFireEventArgs() { score = CalculateScore(), accuracy = CalculateAccuracy() });
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
                    return;
                }
                timeNotTrackingObstacle += Time.deltaTime;
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

    private float CalculateScore() {
        score += (BASE_SCORE * CalculateAccuracy()) / 2;
        return score;
    }
}
