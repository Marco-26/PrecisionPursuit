using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject gamemodes;
    [SerializeField] private GameObject gamemodeSettings;

    void Update()
    {
        SetGamemode();
    }

    public void OnClick_GamemodeSettings() {
        gamemodes.SetActive(false);
        gamemodeSettings.SetActive(true);
    }

    public void OnClick_Back() {
        gamemodes.SetActive(true);
        gamemodeSettings.SetActive(false);
    }
    public void OnClick_Play() {
        SceneManager.LoadScene("HeadshotPractice");
    }

    void SetGamemode() {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer") 
            GameValues.GamemodeSTR = "Timer";
        else if (value == "Destroy X obstacles") 
            GameValues.GamemodeSTR = "KillAmount";
    }

    
}
