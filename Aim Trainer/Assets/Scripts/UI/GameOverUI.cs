using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("Game Over UI")]
        [SerializeField] private GameObject recordBeatenMessage;
        [SerializeField] private TextMeshProUGUI scoreTextGameOver;
        [SerializeField] private TextMeshProUGUI accuracyTextGameOver;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private PlayerGun playerGun;
        
        private void Start() {
            this.gameObject.SetActive(false);
            recordBeatenMessage.SetActive(false);

            GameManager.Instance.OnGameEnd += GameManager_OnGameEnd;
            HandleButtonListeners();
        }
        
        private void HandleButtonListeners() {
            restartButton.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
                GameManager.Instance.RestartGame();
            });

            mainMenuButton.onClick.AddListener(() => {
                this.gameObject.SetActive(false);
                SceneManager.LoadScene("MainMenu");
            });
        }
        
        private void GameManager_OnGameEnd(object sender, EventArgs e) {
            this.gameObject.SetActive(true);
            if (GameManager.Instance.IsHighscoreBeaten()) {
                recordBeatenMessage.SetActive(true);
            }
            scoreTextGameOver.text = "Score: " + (int)playerGun.GetScore();
            accuracyTextGameOver.text = "Accuracy: " + (int)playerGun.GetAccuracy();
        }
    }
}