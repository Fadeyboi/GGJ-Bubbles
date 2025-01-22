using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverText;
    public static int Score {get; private set;} = 0;
    private bool gameEnded;
    public TextMeshProUGUI scoreText;

    public static bool GlobalGameEnded { get; private set; }

    void Awake()
    {
        gameEnded = false;
        gameOverText.SetActive(false);
        Score = 0;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update(){
        if (gameEnded && Input.GetKeyDown(KeyCode.X)){
            QuitToMainMenu();
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {Score}";
        }
    }

    public void EndGame(){
        Debug.Log("Game Ended");
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Debug.Log("Game Paused");
        gameEnded = true;
        GlobalGameEnded = gameEnded;
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }

    void QuitToMainMenu(){
        Debug.Log("Quitting to main menu");
        //SceneManager.LoadScene("Main Menu");
    }
}
