using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour{

    public event EventHandler OnPauseKeyPressed;

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            OnPauseKeyPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
