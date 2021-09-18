using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// 
/// </summary>
public class UIManager : MonoSingletonGeneric<UIManager>
{
    public static UIManager instance;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject YouWinPanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private static int Score;

    private void Start()
    {
        Score = 0;
        scoreText.text = "Score: " + Score.ToString();
    }

    public void OnClickRestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickMenuBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadNextRound()
    {
        YouWinPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        PlayerService.Instance.GetPlayerController().PlayerDied(false);
        PlayerService.Instance.CreatePlayer();
        EnemySpawner.Instance.spawnOfEnemies();
    }

    public void OnClickStartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickQuitBtn()
    {
        Application.Quit();
    }

    public void ShowGameOverScreen()
    {
        GameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        finalScoreText.text = "Your Final Score is: " + Score.ToString();
    }

    public void ShowWinScreen()
    {
        if (YouWinPanel)
        {
            YouWinPanel.SetActive(true);
        }
    }

    public void UpdateScore(int _score)
    {
        // Score += 10;
        Score += _score;
        scoreText.text = "Score: " + Score.ToString();
    }

    // bool paused = false;
    // public void Pause()
    // {
    //     if (paused)
    //     {
    //         paused = false;
    //         Time.timeScale = 1;
    //     }
    //     else
    //     {
    //         paused = true;
    //         Time.timeScale = 0;
    //     }
    // }
}
