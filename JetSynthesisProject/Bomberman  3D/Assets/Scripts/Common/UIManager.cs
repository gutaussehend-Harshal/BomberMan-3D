using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This class handles UI manager of the game
/// </summary>

namespace JetSynthesis.BomberMan3D
{
    public class UIManager : MonoSingletonGeneric<UIManager>
    {
        [Header("UI Settings")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWinPanel;
        [SerializeField] private GameObject gamePausePanel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private string currentScene;
        [SerializeField] private string menuScene;
        private static int score;
        public static int health;
        private bool paused = false;

        // This finction is called when an object is enable
        private void OnEnable()
        {
            EventService.onScoreIncreased += ScoreUpdate;
            EventService.onScoreIncreased += UpdateHealth;
        }

        private void Start()
        {
            SetUIValues();
        }

        // This method sets the initial values
        private void SetUIValues()
        {
            score = 0;
            health = 50;
            healthText.text = "Health: " + health.ToString();
            scoreText.text = "Score: " + score.ToString();
        }

        private void ScoreUpdate()
        {
            UpdateScore(10);
        }

        private void UpdateHealth()
        {
            UpdateHealth(10);
        }

        //  This method used for restart the game
        public void OnClickRestartBtn()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            SceneManager.LoadScene(currentScene);
        }

        // This method used for go back to main menu
        public void OnClickMenuBtn()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            SceneManager.LoadScene(menuScene);
        }

        // This method used for loading a next level
        public void LoadNextRound()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            SetUIValues();
            playerWinPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            EnemySpawner.Instance.spawnOfEnemies();
        }

        // This method used for start the game
        public void OnClickStartBtn()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            SceneManager.LoadScene(currentScene);
        }

        // This method used for quit the game
        public void OnClickQuitBtn()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            Application.Quit();
        }

        // After player died, this method shows game over panel
        public void ShowGameOverScreen()
        {
            gameOverPanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
            finalScoreText.text = "Your Final Score is: " + score.ToString();
        }

        // This method shows game win panel
        public void ShowWinScreen()
        {
            if (playerWinPanel)
            {
                SoundManager.Instance.Play(Sounds.levelWin);
                playerWinPanel.SetActive(true);
            }
        }

        // This method used for update the player score
        public void UpdateScore(int _score)
        {
            score += _score;
            scoreText.text = "Score: " + score.ToString();
        }

        // This method used for update the player health
        public void UpdateHealth(int _health)
        {
            health -= _health;
            healthText.text = "Health: " + health.ToString();
        }

        // This method used for pause the game
        public void OnClickPauseBtn()
        {
            SoundManager.Instance.Play(Sounds.buttonClick);
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
                gamePausePanel.SetActive(false);
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                gamePausePanel.SetActive(true);
            }
        }

        private void OnDisable()
        {
            EventService.onScoreIncreased -= ScoreUpdate;
            EventService.onHealthUpdate -= UpdateHealth;
        }
    }
}