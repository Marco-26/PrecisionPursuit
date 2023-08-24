using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerGun : MonoBehaviour, IUnit {
    private const int BASE_SCORE = 10;

    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;

    private int totalShotsFired = 0;
    private int totalShotsHit = 0;
    private float timeTrackingObstacle = 0f;
    private float timeNotTrackingObstacle = 0f;
    private float score = 0;
    private float highscore;
    private Transform obstacle = null;

    public event EventHandler<FireEventArgs> OnShotsFired;
    public event EventHandler<FireEventArgs> OnTrackedObstacle;


    public class FireEventArgs : EventArgs {
        public float score;
        public float accuracy;
    }

    private void Update() {
        if (GameManager.Instance.GetCurrentGamemode() == Gamemode.MOTION_SHOT) {
            Track();
            return;
        }

        if (Input.GetButtonDown("Fire1")) {
            SoundManager.Instance.PlaySound(SoundManager.Sound.WeaponShoot);
            Shoot();
        }
    }

    private void Shoot() {
        totalShotsFired++;
        float tempScore = score;
        OnShotsFired?.Invoke(this, new FireEventArgs() { score = CalculateScore(false), accuracy = CalculateAccuracy() });

        if (Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null) {
                SoundManager.Instance.PlaySound(SoundManager.Sound.ObstacleHit);
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
                target.SetIsNotBeingTracked();
            }
            obstacle = null;
        }

        if (Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            obstacle = hit.transform;
            if (obstacle != null) {
                MovingObstacle target = obstacle.GetComponent<MovingObstacle>();
                if (target != null) {
                    timeTrackingObstacle += Time.deltaTime;
                    target.SetIsBeingTracked();
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
            case Gamemode.GRIDSHOT:
                if (totalShotsFired <= 0) {
                    return 0;
                }
                return ((float)totalShotsHit / totalShotsFired) * 100;
            case Gamemode.MOTION_SHOT:
                float totalTime = timeTrackingObstacle + timeNotTrackingObstacle;
                if (totalTime <= 0) {
                    return 0;
                }

                return (timeTrackingObstacle / totalTime) * 100;
        }
        return 0;
    }

    private float CalculateScore(bool hit) {
        if (hit) {
            if (GameManager.Instance.GetCurrentGamemode() == Gamemode.GRIDSHOT) {
                score += (BASE_SCORE * CalculateAccuracy()) / 2;
                return score;
            }

            score += (CalculateAccuracy()) / 15;
            return score;
        } else {
            if (score <= 0) {
                return 0;
            }

            if (GameManager.Instance.GetCurrentGamemode() == Gamemode.GRIDSHOT) {
                score -= (BASE_SCORE * CalculateAccuracy()) / 4;
                return score;
            }

            score -= (CalculateAccuracy()) / 40;
            return score;
        }
    }

    public float GetScore() {
        return score;
    }

    public void SetHighscore(float score) {
        highscore = score;
        Debug.Log("Loaded highscore: " + score);
    }

    public float GetHighscore() {
        Debug.Log("Get highscore: "+ highscore);
        return highscore;
    }
}
