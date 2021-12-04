using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Windows")]
    public GameObject playWindow;
    public GameObject settingsWindow;

    [Header("Scenes")]
    public int gameSceneIndex = 1;

    [Header("Play Menu")]
    public GameObject savePrefab;
    public Transform listParent;

    private void Start()
    {
        ChangeWindow("Main");
    }

    private void Update()
    {
        if (playWindow.activeSelf)
        {
            LoadGameSaves();
        }
    }

    public void ChangeWindow(string window)
    {
        playWindow.SetActive(false);
        settingsWindow.SetActive(false);

        switch (window)
        {
            case "Main":
                break;

            case "Play":
                playWindow.SetActive(true);
                break;

            case "Settings":
                settingsWindow.SetActive(true);
                break;
        }
    }

    public void LoadGame(GameSave save)
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGameSaves()
    {
        if (!SavingSystem.DirExists("Saves"))
            SavingSystem.CreateDir("Saves");

        foreach (Transform row in listParent)
        {
            Destroy(row.gameObject);
        }

        foreach (string file in SavingSystem.GetDirectory("Saves"))
        {
            GameSave save = SavingSystem.LoadData<GameSave>($"Saves/{SavingSystem.GetFileName(file)}");

            GameSaveRow row = Instantiate(savePrefab, listParent).GetComponent<GameSaveRow>();
            row.SetText(save.GameName, $"{save.saveTime} - {save.saveDate}");
        }
    }
}
