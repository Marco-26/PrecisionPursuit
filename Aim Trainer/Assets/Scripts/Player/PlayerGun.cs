using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera cams;

    void FixedUpdate()
    {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        RaycastHit hit;
        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out hit, range)) {
            //TODO - contar os tiros acertados e os tiros falhados, para fazer uma média de acerto.
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                GameManager.instance.obstacleKillCount++;
            }
        }
    }
}
