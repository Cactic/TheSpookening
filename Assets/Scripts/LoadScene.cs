using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void CreditsActive()
    {
        SceneManager.LoadScene("Credits");
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Quit()
    {
        Application.Quit();
    }


}
