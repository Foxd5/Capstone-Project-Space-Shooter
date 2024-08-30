using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); // Replace "MainScene" with the name of your gameplay scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}