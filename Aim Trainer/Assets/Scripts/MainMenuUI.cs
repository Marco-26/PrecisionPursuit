using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private GameObject amountOptions;
    private GameManager gameManager;

    void Update()
    {
        SetGamemode();
        DisplayAmounts();
    }

    public void OnClick_Play() {
        SceneManager.LoadScene("Practice");
    }

    private void DisplayAmounts() {
        if(dropdown.value == 0) {
            amountOptions.SetActive(false);
        }
        else {
            amountOptions.SetActive(true);
        }
    }

    private void SetGamemode()
    {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer Based")
            MainMenuManager.instance.SetGamemode(Gamemode.TIMER_BASED);
        else if (value == "Target Based")
            MainMenuManager.instance.SetGamemode(Gamemode.TARGET_BASED);
        else
            MainMenuManager.instance.SetGamemode(Gamemode.UNSELECTED);

    }

    public void OnClick_TargetAmountButton(int amount)
    {
        MainMenuManager.instance.SetTotalTargets(amount);
    }
}
