using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager {

    public static void Load(Gamemode gamemode, out float savedHighscore) {
        if(gamemode == Gamemode.FLICKING) {
            savedHighscore = PlayerPrefs.GetFloat("flickingGamemodeHighscore", 0);
            return;
        }
        savedHighscore = PlayerPrefs.GetFloat("targetingGamemodeHighscore", 0);
    }

    public static void Save(Gamemode gamemode, float highscoreRecord) {
        if (gamemode == Gamemode.FLICKING) {
            PlayerPrefs.SetFloat("flickingGamemodeHighscore", highscoreRecord);
        } else {
            PlayerPrefs.SetFloat("targetingGamemodeHighscore", highscoreRecord);
        }
        PlayerPrefs.Save();
    }
}