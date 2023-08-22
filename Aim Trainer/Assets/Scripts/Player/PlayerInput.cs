using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{

    public static PlayerInput Instance { get; private set; }    

    public event EventHandler OnPauseKeyPressed;

    private Vector2 sensitivity;

    public event EventHandler<SensitivityArgs> OnSensitivityChanged;

    public class SensitivityArgs : EventArgs {
        public Vector2 newSensitivity;
    }

    private void Awake() {
        Instance = this;
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
