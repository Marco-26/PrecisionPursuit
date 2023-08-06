using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour{
    public static SaveManager Instance { get; private set; }

    private float savedHighscore;

    private void Awake() {
        Instance = this;
    }

    public void Load(Gamemode gamemode) {
        if(gamemode == Gamemode.FLICKING) {
            savedHighscore = PlayerPrefs.GetFloat("flickingGamemodeHighscore", 0);
            return;
        }
        savedHighscore = PlayerPrefs.GetFloat("targetingGamemodeHighscore", 0);
    }

    public void Save(Gamemode gamemode, float highscoreRecord) {
        savedHighscore = highscoreRecord;
        if (gamemode == Gamemode.FLICKING) {
            PlayerPrefs.SetFloat("flickingGamemodeHighscore", highscoreRecord);
        } else {
            PlayerPrefs.SetFloat("targetingGamemodeHighscore", highscoreRecord);
        }
        PlayerPrefs.Save();
    }

    public float GetSavedHighscore() {
        return savedHighscore;
    }
}
