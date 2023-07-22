using UnityEngine;
using System;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;
    
    private int totalShotsFired = 0;
    private int totalShotsHit = 0;

    public event EventHandler<ShotsFiredEventArgs> OnShotsFired;
    public event EventHandler<ShotsHitEventArgs> OnShotsHit;

    public class ShotsFiredEventArgs : EventArgs{
        public int totalShotsFired;
    }

    public class ShotsHitEventArgs : EventArgs {
        public int totalShotsHit;
    }

    //TODO: Calcular a accuracy aqui e mandar um evento para o GameManager ja com isso calculado
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        totalShotsFired++;
        OnShotsFired?.Invoke(this, new ShotsFiredEventArgs() { totalShotsFired = totalShotsFired }); ;

        RaycastHit hit;
        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out hit, range)) {
            //TODO - contar os tiros acertados e os tiros falhados, para fazer uma média de acerto.
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                totalShotsHit++;
                OnShotsHit?.Invoke(this, new ShotsHitEventArgs() { totalShotsHit = totalShotsHit });
            }
        }
    }
}
