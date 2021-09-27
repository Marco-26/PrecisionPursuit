using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    // Update is called once per frame
    void Update()
    {
        GetGamemode();
    }

    void GetGamemode() {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer") 
            GameValues.GamemodeSTR = "Timer";
        else if (value == "Destroy X obstacles") 
            GameValues.GamemodeSTR = "KillAmount";
    }

    public void Play() {
        SceneManager.LoadScene("TapPractice");
    }
}
