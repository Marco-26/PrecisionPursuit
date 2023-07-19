using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float range = 150f;
    [SerializeField] private Camera cams;

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        GameManager.instance.totalShots++;

        RaycastHit hit;
        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out hit, range)) {
            //TODO - contar os tiros acertados e os tiros falhados, para fazer uma média de acerto.
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                GameManager.instance.obstaclesDestroyed++;
            }
        }
    }
}
