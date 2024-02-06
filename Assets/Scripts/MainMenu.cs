using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void PlayGame()
    {
        ItemCollector.score = 0;
        Timer.currentTime = 0f;
        SceneManager.LoadScene("Level 1");
    }
    public void PlayTutorial()
    {
        ItemCollector.score = 0;
        Timer.currentTime = 0f;
        SceneManager.LoadScene("Tutorial");
    }

    public void HighScores()
    {

    }

    public void GoToSettings()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
