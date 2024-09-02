using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlPanel;
    public GameObject creditPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); 
    }

    public void ShowCredits()
    {
        creditPanel.SetActive(true);
    }
    public void HideCredits()
    {
        creditPanel.SetActive(false);
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