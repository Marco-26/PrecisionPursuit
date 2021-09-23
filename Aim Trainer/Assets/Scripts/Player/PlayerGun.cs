using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera cams;
    [SerializeField] private int obstacleCount;

    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot() {
        RaycastHit hit;
        if(Physics.Raycast(cams.transform.position, cams.transform.forward, out hit, range)) {
            Obstacle target = hit.transform.GetComponent<Obstacle>();
            if (target != null){
                target.Destroy();
                obstacleCount++;
                Debug.Log(obstacleCount);
            }
        }
    }
}
