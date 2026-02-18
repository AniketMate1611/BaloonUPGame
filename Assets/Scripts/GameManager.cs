using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    BoxCollider2D scoreCollider;
    [SerializeField]TextMeshProUGUI scoreText;
     bool isGameFinished = false;
    bool isGameOver = false;
    public int score { get; private set; } = 0;
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        scoreCollider = GetComponent<BoxCollider2D>();
        Time.timeScale = 1.0f;  
      
    }
    private void Update()
    {
        if (isGameFinished || isGameOver)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(0);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(0);
            }
        }
        if (score >= 100)
        {
            FinishGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            score++;
            scoreText.text = "Score : " + score;
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;

    }
    void FinishGame()
    {
        isGameFinished = true;
        Time.timeScale = 0;
    }
}
