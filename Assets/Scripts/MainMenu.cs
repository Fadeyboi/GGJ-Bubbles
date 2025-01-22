using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas learnHowToPlayCanvas;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Learn()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        learnHowToPlayCanvas.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        learnHowToPlayCanvas.gameObject.SetActive(false);
    }
}
