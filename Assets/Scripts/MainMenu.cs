using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
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
        GameObject PlayButton = GameObject.FindWithTag("PlayButton");
        GameObject QuitButton = GameObject.FindWithTag("QuitButton");
        GameObject LearnButton = GameObject.FindWithTag("LearnButton");
        GameObject HowToPlay = GameObject.FindWithTag("HowToPlay");
        GameObject BackButton = GameObject.FindWithTag("BackButton");
        Debug.Log(HowToPlay);

        PlayButton.SetActive(false);
        QuitButton.SetActive(false);
        LearnButton.SetActive(false);

        //HowToPlay.SetActive(true);
        //BackButton.SetActive(true);
    }

    public void BackToMainMenu()
    {
        GameObject PlayButton = GameObject.FindWithTag("PlayButton");
        GameObject QuitButton = GameObject.FindWithTag("QuitButton");
        GameObject LearnButton = GameObject.FindWithTag("LearnButton");
        GameObject HowToPlay = GameObject.FindWithTag("HowToPlay");
        GameObject BackButton = GameObject.FindWithTag("BackButton");

        if (HowToPlay) HowToPlay.SetActive(false);
        if (BackButton) BackButton.SetActive(false);

        if (PlayButton) PlayButton.SetActive(true);
        if (QuitButton) QuitButton.SetActive(true);
        if (LearnButton) LearnButton.SetActive(true);
    }
}
