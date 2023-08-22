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

    [SerializeField] private TextMeshProUGUI xAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI yAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderPercentage;

    [SerializeField] private Slider xAxisSensitivitySlider;
    [SerializeField] private Slider yAxisSensitivitySlider;

    private float minSliderValue = 0.1f;
    private float maxSliderValue = 100.0f;

    private void Awake() {
        Instance = this; 
    }

    private void Start(){
        gameObject.SetActive(false);

        xAxisSensitivitySlider.value  = minSliderValue;
        yAxisSensitivitySlider.value = minSliderValue;
        xAxisSensitivitySliderPercentage.text = minSliderValue.ToString() + "%";
        yAxisSensitivitySliderPercentage.text = minSliderValue.ToString() + "%";
        
        transform.Find("resumeBtn").GetComponent<Button>().onClick.AddListener(() => {
            SetSensitivityBasedOnSlider();
            GameManager.Instance.TogglePauseGame();
        });

        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("MainMenu");
        });

        HandleCrosshairButtons();
    }

    public void ManipulateXAxisSensitivitySliderPercentage(float value) {
        float final = value * maxSliderValue;
        xAxisSensitivitySliderPercentage.text = final.ToString("0") + "%";
    }

    public void ManipulateYAxisSensitivitySliderPercentage(float value) {
        float final = value * maxSliderValue;
        yAxisSensitivitySliderPercentage.text = final.ToString("0") + "%";
    }

    public void ManipulateSoundEffectsSliderPercentage(float value) {
        float final = value * maxSliderValue;
        soundEffectsSliderPercentage.text = final.ToString("0") + "%";
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

    private void SetSensitivityBasedOnSlider() {
        PlayerInput.Instance.SetSensitivity(new Vector2(xAxisSensitivitySlider.value, yAxisSensitivitySlider.value));
    }
}
