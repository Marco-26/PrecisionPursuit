using UnityEngine;

public class PlayerLook : MonoBehaviour {
    public static bool cursorLock = true;

    [SerializeField] private Transform player;
    [SerializeField] private Transform cams;
    [SerializeField] private float maxAngle;

    private Quaternion camCenter;
    private float xSensitivity;
    private float ySensitivity;
    private int sensitivityMultiplier = 3;
    private float defaultSensitivityValue = 0.5f;

    private void Awake() {
        PlayerInput.Instance.OnSensitivityChanged += PlayerInput_OnSensitivityChanged;
    }

    void Start()
    {
        xSensitivity = defaultSensitivityValue * sensitivityMultiplier;
        ySensitivity = defaultSensitivityValue * sensitivityMultiplier;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camCenter = cams.localRotation;

    }

    private void PlayerInput_OnSensitivityChanged(object sender, PlayerInput.SensitivityArgs e) {
        xSensitivity = e.newSensitivity.x * sensitivityMultiplier;
        ySensitivity = e.newSensitivity.y * sensitivityMultiplier;
    }


    void Update()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }

    void SetY() {
        float t_input = Input.GetAxisRaw("Mouse Y") * ySensitivity;
        Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adj;

        if (Quaternion.Angle(camCenter, t_delta) < maxAngle) {
            cams.localRotation = t_delta;
        }
    }
    void SetX() {
        float t_input = Input.GetAxisRaw("Mouse X") * xSensitivity;
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
