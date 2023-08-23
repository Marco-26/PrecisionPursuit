using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { get; private set; }
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadPlayerPreferences();
    }

    public void LoadPlayerPreferences() {
        if(!HasPlayerPrefsSave()) {
            Debug.Log("No saves found");
            return;
        }

        Vector2 storedSensitivity = new Vector2(PlayerPrefs.GetFloat("sensitivityX"), PlayerPrefs.GetFloat("sensitivityY"));
        float storeSoundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");
        string crosshairTypeString = PlayerPrefs.GetString("crosshairType");
        Enum.TryParse(crosshairTypeString, out CrosshairType crosshairType);

        PlayerInput.Instance.SetSensitivity(storedSensitivity);
        SoundManager.Instance.ChangeVolume(storeSoundEffectsVolume);
        UIManager.Instance.ChangeCrosshairUIByType(crosshairType);

        OptionsUI.Instance.ChangeSlidersValues(storedSensitivity.x, storedSensitivity.y, storeSoundEffectsVolume);

        Debug.Log("Loaded Player Prefs");
    }

    public void SavePlayerPreferences(float soundEffectsVolume, Vector2 sensitivity, CrosshairType crosshairType) {
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);
        PlayerPrefs.SetFloat("sensitivityX", sensitivity.x);
        PlayerPrefs.SetFloat("sensitivityY", sensitivity.y);
        PlayerPrefs.SetString("crosshairType", crosshairType.ToString());

        PlayerPrefs.Save();
        Debug.Log("Saved player prefs");
    }

    public bool HasPlayerPrefsSave() {
        return PlayerPrefs.HasKey("soundEffectsVolume");
    }

    public Gamemode LoadGamemodePref() {
        if(PlayerPrefs.GetString("gamemode") == Gamemode.GRIDSHOT.ToString()) {
            return Gamemode.GRIDSHOT;
        }

        return Gamemode.NULL;
    }
}
