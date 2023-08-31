using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour{

    public static PlayerManager Instance { get; private set; }    

    public event EventHandler OnPauseKeyPressed;
    private Vector2 sensitivity;
    private PlayerGun playerGun;
    private PlayerLook playerLook;
    
    public event EventHandler<SensitivityArgs> OnSensitivityChanged;

    public class SensitivityArgs : EventArgs {
        public Vector2 newSensitivity;
    }

    private void Awake() {
        Instance = this;
        playerGun = GetComponent<PlayerGun>();
        playerLook = GetComponent<PlayerLook>();
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnPauseKeyPressed?.Invoke(this, EventArgs.Empty);
        }
    }

    public void SetSensitivity(Vector2 newSensitivity) {
        sensitivity = newSensitivity;
        OnSensitivityChanged?.Invoke(this, new SensitivityArgs() { newSensitivity = sensitivity });
    }
}
