using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button flickingGamemodeButton;
    [SerializeField] private Button trackingGamemodeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject selectGamemodeUI;

    private void Awake() {
        flickingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(GamemodeScenes.Flicking.ToString());
            SaveManager.SaveChosenGamemode(Gamemode.FLICKING);
        });

        trackingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(GamemodeScenes.Tracking.ToString());
            SaveManager.SaveChosenGamemode(Gamemode.TRACKING);
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
