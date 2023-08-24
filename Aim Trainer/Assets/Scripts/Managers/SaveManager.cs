using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { get; private set; }

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    [SerializeField]private GameObject unitGameObject;
    private IUnit unit;
    
    private void Awake() {
        Instance = this;
        unit = unitGameObject.GetComponent<IUnit>();

        if (!Directory.Exists(SAVE_FOLDER)) {
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        LoadSensibleData();
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

    public void LoadSensibleData() { 
        if(File.Exists(SAVE_FOLDER + "/save.txt")) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            Debug.Log(GameManager.Instance.GetCurrentGamemode());
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            
            unit.SetHighscore(saveObject.gridshotHighscore);
        }
    }

    public void SaveSensibleData() {
        float currentScore = unit.GetScore();

        SaveObject saveObject = new SaveObject {
            gridshotHighscore = currentScore
        };

        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);
    }

    private class SaveObject {
        public float gridshotHighscore;
    }
}

