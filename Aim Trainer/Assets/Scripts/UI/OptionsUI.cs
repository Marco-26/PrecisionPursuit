using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Assertions.Must;

public class OptionsUI : MonoBehaviour{

    public static OptionsUI Instance { get; private set; }

    [Header("Crosshair")]
    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    [SerializeField] private Button crosshairSmallBtn;
    [SerializeField] private Button crosshairMediumBtn;
    [SerializeField] private Button crosshairLargeBtn;
    
    [Header("Sensitivity")]
    [SerializeField] private TextMeshProUGUI xAxisSensitivitySliderPercentage;
    [SerializeField] private TextMeshProUGUI yAxisSensitivitySliderPercentage;
    [SerializeField] private Slider xAxisSensitivitySlider;
    [SerializeField] private Slider yAxisSensitivitySlider;
    [SerializeField] private Toggle toggleMatchBothAxis;
    
    [Header("Sound")]
    [SerializeField] private TextMeshProUGUI soundEffectsSliderPercentage;
    [SerializeField] private Slider soundEffectsSlider;
    [SerializeField] private Toggle toggleMute;

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
            SceneManager.LoadScene(GamemodeScenes.MainMenu.ToString());
        });
        
        HandleCrosshairSection();
        HandleSensitivitySection();
        HandleAudioSection();
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

    private void HandleCrosshairSection() {
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

    private void HandleSensitivitySection()
    {
        // Add initial listeners for X and Y sliders
        xAxisSensitivitySlider.onValueChanged.AddListener(delegate
        {
            ManipulateXAxisSensitivitySliderPercentage(xAxisSensitivitySlider.value);
        });

        yAxisSensitivitySlider.onValueChanged.AddListener(delegate
        {
            ManipulateYAxisSensitivitySliderPercentage(yAxisSensitivitySlider.value);
        });

        // Add listener for the toggle
        toggleMatchBothAxis.onValueChanged.AddListener(delegate
        {
            ToggleMatchBothAxis();
        });
    }
    
    private void ToggleMatchBothAxis()
    {
        bool matchAxis = toggleMatchBothAxis.isOn;

        if (matchAxis)
        {
            Debug.Log("Toggle match axis is on");

            // Add listeners to both sliders that match their values
            xAxisSensitivitySlider.onValueChanged.AddListener(delegate
            {
                ManipulateXAxisSensitivitySliderPercentage(xAxisSensitivitySlider.value);
                ManipulateYAxisSensitivitySliderPercentage(xAxisSensitivitySlider.value);
                yAxisSensitivitySlider.value = xAxisSensitivitySlider.value;
            });

            yAxisSensitivitySlider.onValueChanged.AddListener(delegate
            {
                ManipulateYAxisSensitivitySliderPercentage(yAxisSensitivitySlider.value);
                ManipulateXAxisSensitivitySliderPercentage(yAxisSensitivitySlider.value);
                xAxisSensitivitySlider.value = yAxisSensitivitySlider.value;
            });
        }
        else
        {
            Debug.Log("Toggle match axis is off");

            // Remove all listeners from both sliders
            xAxisSensitivitySlider.onValueChanged.RemoveAllListeners();
            yAxisSensitivitySlider.onValueChanged.RemoveAllListeners();

            // Re-add the initial listeners for X and Y sliders
            xAxisSensitivitySlider.onValueChanged.AddListener(delegate
            {
                ManipulateXAxisSensitivitySliderPercentage(xAxisSensitivitySlider.value);
            });

            yAxisSensitivitySlider.onValueChanged.AddListener(delegate
            {
                ManipulateYAxisSensitivitySliderPercentage(yAxisSensitivitySlider.value);
            });
        }
    }

    private void HandleAudioSection()
    {
        soundEffectsSlider.onValueChanged.AddListener(delegate
        {
            ManipulateSoundEffectsSliderPercentage(soundEffectsSlider.value);
        });
        
        toggleMute.onValueChanged.AddListener(delegate
        {
            if (toggleMute.isOn)
            {
                soundEffectsSlider.interactable = false;
                SoundManager.Instance.MuteAudio();
                return;
            }
            
            soundEffectsSlider.interactable = true;
            SoundManager.Instance.ChangeVolume(soundEffectsSlider.value);
        });
    }

    private void SetSensitivityBasedOnSlider() {
        PlayerManager.Instance.SetSensitivity(new Vector2(xAxisSensitivitySlider.value, yAxisSensitivitySlider.value));
    }

    private void SetAudioBasedOnSlider() {
        if (!toggleMute.isOn)
        {
            SoundManager.Instance.ChangeVolume(soundEffectsSlider.value);
        }
    }
}
