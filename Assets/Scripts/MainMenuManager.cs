using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Windows")]
    public GameObject settingsWindow;

    [Header("Scenes")]
    public int gameSceneIndex = 1;

    private void Start()
    {
        ChangeWindow("Main");
    }

    public void ChangeWindow(string window)
    {
        settingsWindow.SetActive(false);

        switch (window)
        {
            case "Main":
                break;

            case "Settings":
                settingsWindow.SetActive(true);
                break;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
