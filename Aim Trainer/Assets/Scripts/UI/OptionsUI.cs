using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsUI : MonoBehaviour{

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    [SerializeField] private Button crosshairSmallBtn;
    [SerializeField] private Button crosshairMediumBtn;
    [SerializeField] private Button crosshairLargeBtn;

    [SerializeField] private TextMeshProUGUI xAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI yAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderPercentage;

    [SerializeField] private Slider xAxisSensitivitySlider;
    [SerializeField] private Slider yAxisSensitivitySlider;
    [SerializeField] private Slider soundEffectsSlider;

    private float maxSliderValue = 100f;

    private void Awake() {
        Instance = this; 
    }

    private void Start(){
        gameObject.SetActive(false);

        transform.Find("resumeBtn").GetComponent<Button>().onClick.AddListener(() => {
            SetSensitivityBasedOnSlider();
            SetAudioBasedOnSlider();
            SaveManager.Instance.SavePlayerPreferences(soundEffectsSlider.value, new Vector2(xAxisSensitivitySlider.value, yAxisSensitivitySlider.value), HudUI.Instance.GetCurrentCrosshairType());
            
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

    public void ChangeSlidersValues(float sensitivityX, float sensitivityY, float soundEffectsVolume) {
        xAxisSensitivitySlider.value = sensitivityX;
        yAxisSensitivitySlider.value = sensitivityY;
        soundEffectsSlider.value = soundEffectsVolume;
    }

    public void ShowOptionsMenu() {
        gameObject.SetActive(true);
    }

    public void HideOptionsMenu() {
        gameObject.SetActive(false);
    }

    private void HandleCrosshairButtons() {
        crosshairSmallBtn.onClick.AddListener(() => {
            HudUI.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[0]);
        });
        crosshairMediumBtn.onClick.AddListener(() => {
            HudUI.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[1]);
        });
        crosshairLargeBtn.onClick.AddListener(() => {
            HudUI.Instance.ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[2]);
        });
    }

    private void SetSensitivityBasedOnSlider() {
        PlayerManager.Instance.SetSensitivity(new Vector2(xAxisSensitivitySlider.value, yAxisSensitivitySlider.value));
    }

    private void SetAudioBasedOnSlider() {
        SoundManager.Instance.ChangeVolume(soundEffectsSlider.value);
    }
}
