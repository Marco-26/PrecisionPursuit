using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject gamemodes;
    [SerializeField] private GameObject gamemodeSettings;

    // Update is called once per frame
    void Update()
    {
        SetGamemode();
    }

    public void OpenGamemodeSettings() {
        gamemodes.SetActive(false);
        gamemodeSettings.SetActive(true);
    }


    void SetGamemode() {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer") 
            GameValues.GamemodeSTR = "Timer";
        else if (value == "Destroy X obstacles") 
            GameValues.GamemodeSTR = "KillAmount";
    }

    
}
