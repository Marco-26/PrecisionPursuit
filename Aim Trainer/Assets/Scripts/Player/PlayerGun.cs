using UnityEngine;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;
    
    private int totalShotsFired = 0;
    private int totalShotsHit = 0;
    private float score = 0;
    private const int BASE_SCORE = 10;

    [SerializeField]private MovingObstacle movingObstacle;


    public event EventHandler<MissFireEventArgs> OnShotsFired;
    public event EventHandler<HitFireEventArgs> OnShotsHit;

    public class HitFireEventArgs : EventArgs{
        public float score;
        public float accuracy;
    }

    public class MissFireEventArgs : EventArgs {
        public float accuracy;
    }

    private void Start() {
    }

    private void Update(){
        if(GameManager.Instance.GetCurrentGamemode() == Gamemode.MOTION) {
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
        if (Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            MovingObstacle target = hit.transform.GetComponent<MovingObstacle>();
            if (target != null) {
                target.ChangeColorWhenTracked();
                return;
            }
            movingObstacle.ResetColor();
        }
    }

    private float CalculateAccuracy() {
        if (totalShotsFired <= 0) {
            return 0;
        }
        return ((float)totalShotsHit / totalShotsFired) * 100;
    }

    private float CalculateScore() {
        score += (BASE_SCORE * CalculateAccuracy()) / 2;
        return score;
    }
}
