using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    public static HudUI Instance { get; private set; }
    
    [Header("In Game UI")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI accuracyText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RectTransform crosshairSprite;
    
    [Header("Additional Options UI")]
    [SerializeField] private bool aditionalOptions;
    [SerializeField] private GameObject additionalOptionsContainer;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI gamemodeText;
    
    [Header("Scripts")]
    [SerializeField] private CrosshairTypeListSO crosshairTypeList;
    [SerializeField] private Timer timer;
    [SerializeField] private PlayerGun playerGun;
    private CrosshairTypeSO currentCrosshair;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        if (!SaveManager.Instance.HasPlayerPrefsSave()) {
            currentCrosshair = crosshairTypeList.crosshairTypeList[0];
        }
        
        if (!aditionalOptions) {
            additionalOptionsContainer.SetActive(false);
        } else {
            additionalOptionsContainer.SetActive(true);
            highscoreText.text = "Highscore: " + GameManager.Instance.GetPlayerHighscore().ToString();
            gamemodeText.text = "Gamemode: " + GameManager.Instance.GetCurrentGamemode().ToString();
        }
        
        playerGun.OnShotsFired += PlayerGun_OnShotsFired;
    }

    private void PlayerGun_OnShotsFired(object sender, PlayerGun.FireEventArgs e)
    {
        DisplayScore((int)e.score);
        DisplayAccuracy((int)e.accuracy);
    }

    private void Update() {
        DisplayTime();
    }
    
    public void ChangeCrosshairUI(CrosshairTypeSO crosshair) {
        currentCrosshair = crosshair;
        crosshairSprite.sizeDelta = new Vector2 (currentCrosshair.width, currentCrosshair.height);
        crosshairSprite.GetComponent<Image>().sprite = currentCrosshair.sprite;
    }
    
    public void ChangeCrosshairUIByType(CrosshairType crosshairType) {
        switch (crosshairType) {
            case CrosshairType.CROSSHAIR_LARGE:
                ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[2]);
                break;
            case CrosshairType.CROSSHAIR_MEDIUM:
                ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[1]);
                break;
            case CrosshairType.CROSSHAIR_SMALL:
                ChangeCrosshairUI(crosshairTypeList.crosshairTypeList[0]);
                break;
        }
    }
    
    public CrosshairType GetCurrentCrosshairType() {
        return currentCrosshair.type;
    }
    
    private void DisplayTime() {
        float timeRemaining = timer.GetTimeRemaining();
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    private void DisplayAccuracy(int accuracy) {
        accuracyText.text = accuracy.ToString();
    }

    private void DisplayScore(int score) {
        scoreText.text = score.ToString();
    }
}
