using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button flickingGamemodeButton;
    [SerializeField] private Button TRACKINGGamemodeButton;

    private const string FLICKING_GAMEMODE_SCENE = "Flicking";
    private const string TRACKING_GAMEMODE_SCENE = "Tracking";

    private void Awake() {
        flickingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(FLICKING_GAMEMODE_SCENE);
        });

        TRACKINGGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(FLICKING_GAMEMODE_SCENE);
        });
    }
}
