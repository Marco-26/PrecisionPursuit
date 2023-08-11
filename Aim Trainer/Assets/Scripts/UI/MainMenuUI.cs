using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button flickingGamemodeButton;
    [SerializeField] private Button trackingGamemodeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameSettignsSO gameSettings; //USAR PLAYER PREFS

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject selectGamemodeUI;

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

        playButton.onClick.AddListener(() => {
            mainMenuUI.SetActive(false);
            selectGamemodeUI.SetActive(true);
        });

        trackingGamemodeButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    private void Start() {
        mainMenuUI.SetActive(true);
        selectGamemodeUI.SetActive(false);
    }
}
