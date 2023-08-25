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

    private SaveObject loadedData;
    
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

    public void LoadSensibleData() { 
        if(File.Exists(SAVE_FOLDER + "/save.txt")) {
            string saveString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            Debug.Log(GameManager.Instance.GetCurrentGamemode());
            loadedData = JsonUtility.FromJson<SaveObject>(saveString);

            if (GameManager.Instance.GetCurrentGamemode() == Gamemode.GRIDSHOT) {
                unit.SetHighscore(loadedData.gridshotHighscore);
            } else {
                unit.SetHighscore(loadedData.motionshotHighscore);
            }

            Debug.Log("Loaded object: " + saveString);
        }
    }

    public void SaveSensibleData() {
        float currentScore = unit.GetScore();
        SaveObject saveObject;
        
        if(GameManager.Instance.GetCurrentGamemode() == Gamemode.GRIDSHOT) {
            saveObject = new SaveObject {
                gridshotHighscore = currentScore,
                motionshotHighscore = loadedData != null ?  loadedData.motionshotHighscore : 0
            };
        } else {
            saveObject = new SaveObject {
                gridshotHighscore = loadedData != null ? loadedData.gridshotHighscore : 0,
                motionshotHighscore = currentScore
            };
        }
        
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(SAVE_FOLDER + "/save.txt", json);
    }

    private class SaveObject {
        public float gridshotHighscore;
        public float motionshotHighscore;
    }
}

