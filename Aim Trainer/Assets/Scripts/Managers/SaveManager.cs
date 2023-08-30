using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Data;
using Managers;

public class SaveManager : MonoBehaviour {

    public static SaveManager Instance { get; private set; }
    
    private SaveData data;
    private FileDataManager fileDataManager;
    private List<ISaveable> saveableObjects;


    private void Awake() {
        Instance = this;

        fileDataManager = new FileDataManager();
        saveableObjects = FindSaveableObjects();
        LoadData();
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

        PlayerManager.Instance.SetSensitivity(storedSensitivity);
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

    public void LoadData()
    {
        data = fileDataManager.LoadData();

        if (data == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            data = new SaveData();
        }

        foreach (ISaveable saveable in saveableObjects)
        {
            saveable.LoadData(data);
        }
    }

    public void SaveData() {
        foreach (ISaveable saveable in saveableObjects)
        {
            saveable.SaveData(data);
        }

        fileDataManager.SaveData(data);
    }
    
    private List<ISaveable> FindSaveableObjects()
    {
        IEnumerable<ISaveable> saveableObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        return new List<ISaveable>(saveableObjects);
    }
}



