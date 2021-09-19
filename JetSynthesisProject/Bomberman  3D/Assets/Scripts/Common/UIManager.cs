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
        [SerializeField] private GameObject GameOverPanel;
        [SerializeField] private GameObject YouWinPanel;
        [SerializeField] private GameObject GamePausePanel;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private string currentScene;
        [SerializeField] private string menuScene;
        private static int score;
        public static int health;
        private bool paused = false;

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

        //  This method used for restart the game
        public void OnClickRestartBtn()
        {
            SceneManager.LoadScene(currentScene);
        }

        // This method used for go back to main menu
        public void OnClickMenuBtn()
        {
            SceneManager.LoadScene(menuScene);
        }

        // This method used for loading a next level
        public void LoadNextRound()
        {
            YouWinPanel.SetActive(false);
            GameOverPanel.SetActive(false);
            PlayerService.Instance.GetPlayerController().PlayerDied();
            PlayerService.Instance.CreatePlayer();
            EnemySpawner.Instance.spawnOfEnemies();
        }

        // This method used for start the game
        public void OnClickStartBtn()
        {
            SceneManager.LoadScene(currentScene);
        }

        // This method used for quit the game
        public void OnClickQuitBtn()
        {
            Application.Quit();
        }

        // After player died, this method shows game over panel
        public void ShowGameOverScreen()
        {
            GameOverPanel.SetActive(true);
            scoreText.gameObject.SetActive(false);
            finalScoreText.text = "Your Final Score is: " + score.ToString();
        }

        // This method shows game win panel
        public void ShowWinScreen()
        {
            if (YouWinPanel)
            {
                YouWinPanel.SetActive(true);
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
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
                GamePausePanel.SetActive(false);
            }
            else
            {
                paused = true;
                Time.timeScale = 0;
                GamePausePanel.SetActive(true);
            }
        }
    }
}