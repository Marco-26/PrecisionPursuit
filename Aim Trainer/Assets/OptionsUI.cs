using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsUI : MonoBehaviour{

    private float maxSliderValue = 100.0f;
    public static OptionsUI Instance;

    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    [SerializeField] private Button crosshairSmallBtn;
    [SerializeField] private Button crosshairMediumBtn;
    [SerializeField] private Button crosshairLargeBtn;

    [SerializeField] private TextMeshProUGUI XAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI YAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI GunSoundsSliderPercentage;
    [SerializeField] private TextMeshProUGUI MenuSoundsSliderPercentage;

    private void Awake() {
        Instance = this; 
    }

    private void Start(){
        gameObject.SetActive(false);

        transform.Find("resumeBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });

        HandleCrosshairButtons();
    }

    public void ManipulateXAxisSensitivitySliderPercentage(float value) {
        float final = value * maxSliderValue;
        XAxisSensitivitySliderPercentage.text = final.ToString("0") + "%";
    }

    public void ManipulateYAxisSensitivitySliderPercentage(float value) {
        float final = value * maxSliderValue;
        YAxisSensitivitySliderPercentage.text = final.ToString("0") + "%";
    }

    public void ManipulateGunSoundsSliderPercentage(float value) {
        float final = value * maxSliderValue;
        GunSoundsSliderPercentage.text = final.ToString("0") + "%";
    }

    public void ManipulateMenuSoundsSliderPercentage(float value) {
        float final = value * maxSliderValue;
        MenuSoundsSliderPercentage.text = final.ToString("0") + "%";
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
