using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]TextMeshProUGUI scoreText;
    [SerializeField]TextMeshProUGUI gameOverText;
    [SerializeField] GameObject pauseBtn;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gamePausePanel;

    // Components
    BoxCollider2D scoreCollider;
    AudioSource bgMusicSource;

    // Boolean States
     bool isGameFinished = false;
    bool isGameOver = false;
    bool isGamePaused = false;

    //Score
    public int score { get; private set; } = 0;

    // Instance Variable
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        scoreCollider = GetComponent<BoxCollider2D>();
        bgMusicSource = GetComponent<AudioSource>();

        Time.timeScale = 1.0f;

        gameOverPanel.gameObject.SetActive(false);
        gamePausePanel.gameObject.SetActive(false);
        bgMusicSource.enabled = true;
        pauseBtn.SetActive(true);
    }
    private void Update()
    {
        if (isGameFinished || isGameOver)
        {
            return;
        }

        if (!isGamePaused)
        {
            if(Input.GetKeyUp(KeyCode.Escape)){
                PauseGame();
            }
        }
        else if (isGamePaused)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
               ResumeGame();
            }
        } 

        if (score >= 500)
        {
            FinishGame();
        }
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score;
    }
    public void GameOver()
    {
        isGameOver = true;
        bgMusicSource.enabled = false;
        pauseBtn.SetActive(false);
        Time.timeScale = 0f;
        gameOverText.text = "Game Over";
        gameOverPanel.gameObject.SetActive(true);
    }
    void FinishGame()
    {
        isGameFinished = true;
        bgMusicSource.enabled = false;
        pauseBtn.SetActive(false);
        Time.timeScale = 0;
        gameOverText.text = "Game Finished";
        gameOverPanel.gameObject.SetActive(true);
    }
    public bool isGameStopped()
    {
        return isGameFinished || isGameOver || isGamePaused;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        isGamePaused = true;
        gamePausePanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        isGamePaused = false;
        gamePausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
