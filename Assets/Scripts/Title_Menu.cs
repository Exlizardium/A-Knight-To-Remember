using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Menu : MonoBehaviour
{
    public GameObject creditsPanel;
    public GameObject knight_UI;
    private AudioManager audioManager;

    private void Start()
    {
        creditsPanel = GameObject.Find("Credits panel");
        knight_UI = GameObject.Find("Knight-UI");
        if (creditsPanel != null)
        {
            creditsPanel.SetActive(false);
        }
        audioManager = FindObjectOfType<AudioManager>();

        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RandomRoomGenerator");
        if(audioManager != null)
        {
            audioManager.Play("Main theme");
        }
    }

    public void GameCredits()
    {
        creditsPanel.SetActive(true);
        knight_UI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        audioManager.Stop("Main theme");
        SceneManager.LoadScene("Title Menu");
    }


}
