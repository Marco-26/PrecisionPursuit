using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject[] possibleKillAmountLimits;
    [SerializeField] private GameObject gamemodeSettings;
    private GameManager gameManager;

    private void Start() {
        
        Debug.Log(GameManager.instance);
    }

    void Update()
    {
        SetGamemode();
        ManageKillAmountOptions();
    }

    public void OnClick_Play() {
        SceneManager.LoadScene("Practice");
    }

    private void ManageKillAmountOptions() {
        if(dropdown.value == 0) {
            for (int i = 0; i < possibleKillAmountLimits.Length; i++) {
                possibleKillAmountLimits[i].SetActive(false);
            }
        }
        else {
            for (int i = 0; i < possibleKillAmountLimits.Length; i++) {
                possibleKillAmountLimits[i].SetActive(true);
            }
        }
    }

    private void SetGamemode() {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer Based")
            GameManager.instance.currentGamemode = GameManager.Gamemode.TIMER_BASED;
        else if (value == "Target Based")
            GameManager.instance.currentGamemode = GameManager.Gamemode.TARGET_BASED;
        else {
            GameManager.instance.currentGamemode = GameManager.Gamemode.UNSELECTED;
        }
    }

    public void OnClick_SetMaxAmount(int amount) {
        GameManager.instance.maxKillCount = amount;
    }
}
