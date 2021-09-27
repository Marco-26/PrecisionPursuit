using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;

    // Update is called once per frame
    void Update()
    {
        GetGamemode();
        if (Input.GetKeyDown(KeyCode.P)) { SceneManager.LoadScene("TapPractice"); }
    }

    void GetGamemode() {
        string value = dropdown.options[dropdown.value].text;
        if (value == "Timer") GameValues.GamemodeSTR = "Timer";
        else if (value == "Destroy X obstacles") GameValues.GamemodeSTR = "KillAmount";
        //TODO - create an static script to change the gamemode acording to value
    }
}
