using UnityEngine;
using System;

public class PlayerGun : MonoBehaviour, IUnit {
    private const int BASE_SCORE = 10;

    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;

    private int totalShotsFired = 0;
    private int totalShotsHit = 0;
    private float score = 0;
    private float highscore;

    public event EventHandler<FireEventArgs> OnShotsFired;

    public class FireEventArgs : EventArgs {
        public float score;
        public float accuracy;
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    private void Shoot() {
        SoundManager.Instance.PlaySound(SoundManager.Sound.WeaponShoot);
        totalShotsFired++;
        float tempScore = score;
        OnShotsFired?.Invoke(this, new FireEventArgs() { score = DecreaseScore(), accuracy = CalculateAccuracy() });

        if (Physics.Raycast(cams.transform.position, cams.transform.forward, out RaycastHit hit, range)) {
            IDestroyable target = hit.transform.GetComponent<IDestroyable>();
            if (target != null) {
                SoundManager.Instance.PlaySound(SoundManager.Sound.ObstacleHit);
                target.Destroy();
                totalShotsHit++;

                OnShotsFired?.Invoke(this, new FireEventArgs() { score = IncreaseScore(), accuracy = CalculateAccuracy() });
            }
        }
    }

    private float CalculateAccuracy() {
        if (totalShotsFired <= 0) {
            return 0;
        }
        return ((float)totalShotsHit / totalShotsFired) * 100;
    }

    private float IncreaseScore() {
        float accuracyModifier = CalculateAccuracy();
        float scoreChange = (BASE_SCORE * accuracyModifier);

        
        scoreChange /= 2;

        score += scoreChange;
        return score;
    }

    private float DecreaseScore() {
        if (score <= 0) {
            return 0;
        }

        float accuracyModifier = CalculateAccuracy();
        float scoreChange = (BASE_SCORE * accuracyModifier) / 4;

        scoreChange *= 2; // Divide by 2 to decrease by 4 times

        score -= scoreChange;
        return score;
    }

    public float GetScore() {
        return score;
    }

    public void SetHighscore(float score) {
        Debug.Log("Record carregado: " + score);
        highscore = score;
    }

    public float GetHighscore() {
        return highscore;
    }
}
