using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { get; private set; }
    
    private void Awake() {
        Instance = this;
    }

    private void Start() {
        LoadPlayerPreferences();
    }

    public void LoadPlayerPreferences() {
        if(PlayerPrefs.GetFloat("soundEffectsVolume") == null) {
            Debug.Log("No saves found");
            return;
        }

        Vector2 storedSensitivity = new Vector2(PlayerPrefs.GetFloat("sensitivityX"), PlayerPrefs.GetFloat("sensitivityY"));
        float storeSoundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume");

        PlayerInput.Instance.SetSensitivity(storedSensitivity);
        SoundManager.Instance.ChangeVolume(storeSoundEffectsVolume);

        OptionsUI.Instance.ChangeSlidersValues(storedSensitivity.x, storedSensitivity.y, storeSoundEffectsVolume);
    }

    public void SavePlayerPreferences(float soundEffectsVolume, Vector2 sensitivity) {
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);
        PlayerPrefs.SetFloat("sensitivityX", sensitivity.x);
        PlayerPrefs.SetFloat("sensitivityY", sensitivity.y);
    }

    public static void SaveChosenGamemode(Gamemode gamemode) {
        PlayerPrefs.SetString("gamemode", gamemode.ToString());
    }

    public static Gamemode GetChosenGamemode() {
        if(PlayerPrefs.GetString("gamemode") == "FLICKING") {
            return Gamemode.FLICKING;
        }
        return Gamemode.TRACKING;
    }
}
