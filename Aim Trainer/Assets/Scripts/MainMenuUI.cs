using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField] private Button flickingGamemodeButton;
    [SerializeField] private Button motionGamemodeButton;

    private const string FLICKING_GAMEMODE_SCENE = "Flicking";
    private const string MOTION_GAMEMODE_SCENE = "Motion";

    private void Awake() {
        flickingGamemodeButton.onClick.AddListener(() => {
            SceneManager.LoadScene(FLICKING_GAMEMODE_SCENE);
        });

        motionGamemodeButton.onClick.AddListener(() => {
            Debug.Log("Motion practice");
        });
    }
}
