using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button gridshotGamemodeButton;
    [SerializeField] private Button motionshotGamemodeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject selectGamemodeUI;

    private void Awake() {
        gridshotGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(GamemodeScenes.Gridshot.ToString());
            PlayerPrefs.SetString("gamemode", Gamemode.GRIDSHOT.ToString());
        });

        motionshotGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(GamemodeScenes.Motionshot.ToString());
            PlayerPrefs.SetString("gamemode", Gamemode.MOTION_SHOT.ToString());
        });

        playButton.onClick.AddListener(() => {
            mainMenuUI.SetActive(false);
            selectGamemodeUI.SetActive(true);
        });

        backButton.onClick.AddListener(() => {
            mainMenuUI.SetActive(true);
            selectGamemodeUI.SetActive(false);
        });

        quitButton.onClick.AddListener(() => {
            Quit();
        });
    }

    private void Start() {
        mainMenuUI.SetActive(true);
        selectGamemodeUI.SetActive(false);
    }

    public static void Quit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
#else
                 Application.Quit();
#endif
    }
}
