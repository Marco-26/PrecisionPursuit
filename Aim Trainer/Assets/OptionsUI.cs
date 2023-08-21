using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsUI : MonoBehaviour{

    public static OptionsUI Instance;

    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    [SerializeField] private Button crosshairSmallBtn;
    [SerializeField] private Button crosshairMediumBtn;
    [SerializeField] private Button crosshairLargeBtn;

    private void Awake() {
        Instance = this; 
    }

    void Start(){
        gameObject.SetActive(false);

        transform.Find("resumeBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });

        HandleCrosshairButtons();
    }

    public void ShowOptionsMenu() {
        gameObject.SetActive(true);
    }

    public void HideOptionsMenu() {
        gameObject.SetActive(false);
    }

    private void HandleCrosshairButtons() {
        crosshairSmallBtn.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[0].crosshairImage, crosshairTypeList.crosshairTypeList[0].width, crosshairTypeList.crosshairTypeList[0].height);
        });
        crosshairMediumBtn.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[1].crosshairImage, crosshairTypeList.crosshairTypeList[1].width, crosshairTypeList.crosshairTypeList[1].height);
        });
        crosshairLargeBtn.onClick.AddListener(() => {
            UIManager.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[2].crosshairImage, crosshairTypeList.crosshairTypeList[2].width, crosshairTypeList.crosshairTypeList[2].height);
        });
    }
}
