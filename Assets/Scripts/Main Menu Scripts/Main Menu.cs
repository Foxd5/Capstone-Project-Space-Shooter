using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene"); 
    }

    public void ShowControls()
    {
        controlPanel.SetActive(true);
    }

    public void HideControls()
    {
        controlPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}