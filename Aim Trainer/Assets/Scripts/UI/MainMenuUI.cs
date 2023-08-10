using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button flickingGamemodeButton;
    [SerializeField] private Button trackingGamemodeButton;
    [SerializeField] private GameSettignsSO gameSettings;

    private const string FLICKING_GAMEMODE_SCENE = "Flicking";
    private const string TRACKING_GAMEMODE_SCENE = "Tracking";

    private void Awake() {
        flickingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(FLICKING_GAMEMODE_SCENE);
            gameSettings.chosenGamemode = Gamemode.FLICKING;
        });

        trackingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(TRACKING_GAMEMODE_SCENE);
            gameSettings.chosenGamemode = Gamemode.TRACKING;
        });
    }
}
