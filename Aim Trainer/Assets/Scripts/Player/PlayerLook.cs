using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public static bool cursorLock = true;
    [SerializeField] private Transform player;
    [SerializeField] private Transform cams;

    [SerializeField] private float xSensitivity;
    [SerializeField] private float ySensitivity;
    [SerializeField] private float maxAngle;

    private Quaternion camCenter;

    void Start()
    {
        camCenter = cams.localRotation;
    }

    void Update()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }

    void SetY() {
        float t_input = Input.GetAxisRaw("Mouse Y") * ySensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adj;

        if (Quaternion.Angle(camCenter, t_delta) < maxAngle) {
            cams.localRotation = t_delta;
        }
    }
    void SetX() {
        float t_input = Input.GetAxisRaw("Mouse X") * xSensitivity * Time.deltaTime;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = player.localRotation * t_adj;
        player.localRotation = t_delta;
    }

    void UpdateCursorLock() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            cursorLock = !cursorLock;
        }

        if (cursorLock) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;            
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
