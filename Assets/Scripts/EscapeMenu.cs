using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public bool menuOpened = false;

    [Header("Windows")]
    public GameObject escapeMenuWindow;
    public GameObject settingsWindow;

    private void Update()
    {
        menuOpened = escapeMenuWindow.activeSelf || settingsWindow.activeSelf;

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!menuOpened)
                ChangeWindow("Escape");

            else
                ChangeWindow("Main");

        }

        Cursor.lockState = menuOpened ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void ChangeWindow(string window)
    {
        escapeMenuWindow.SetActive(false);
        settingsWindow.SetActive(false);

        switch (window)
        {
            case "Main":
                break;

            case "Escape":
                escapeMenuWindow.SetActive(true);
                break;

            case "Settings":
                settingsWindow.SetActive(true);
                break;
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
