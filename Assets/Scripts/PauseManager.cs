using UnityEngine;
using TMPro;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseText;
    public static bool GlobalIsPaused { get; private set; }

    void Start()
    {
        // Ensure the pause text is disabled at the start
        if (pauseText != null)
        {
            pauseText.SetActive(false);
        }
    }

    void Update()
    {
        if (GameManager.GlobalGameEnded)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (isPaused && Input.GetKeyDown(KeyCode.X))
        {
            QuitToMainmenu();

        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        GlobalIsPaused = isPaused;
        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Freeze time
        AudioListener.pause = true; // Pause all audio
        Debug.Log("Game Paused");

        // Show the pause text
        if (pauseText != null)
        {
            pauseText.SetActive(true);
        }
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Resume time
        AudioListener.pause = false; // Resume all audio
        Debug.Log("Game Resumed");

        // Hide the pause text
        if (pauseText != null)
        {
            pauseText.SetActive(false);
        }
    }

    void QuitToMainmenu(){
        Debug.Log("Quitting to main menu");
        //SceneManager.LoadScene("Main Menu");
    }
}
