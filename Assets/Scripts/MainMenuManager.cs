using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private static MainMenuManager _instance = null;
    public static MainMenuManager Instance { get { return _instance; } }

    [Header("Windows")]
    public GameObject playWindow;
    public GameObject newGameWindow;
    public GameObject loadGameWindow;
    public GameObject settingsWindow;

    [Header("Scenes")]
    public int gameSceneIndex = 1;

    [Header("New Game Menu")]
    public TMP_InputField gameNameInput;
    public GameObject errorText;

    [Header("Load Game Menu")]
    public GameObject savePrefab;
    public Transform listParent;
    public string selectedSave = "";

    private bool loadedSaves = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        ChangeWindow("Main");
    }

    private void Update()
    {
        if (loadGameWindow.activeSelf && !loadedSaves)
        {
            LoadGameSaves();

            loadedSaves = true;
        }

        if (!loadGameWindow.activeSelf)
        {
            loadedSaves = false;
        }

        errorText.SetActive(SavingSystem.FileExists($"Saves/{gameNameInput.text}.factory"));

        foreach (Transform t in listParent)
        {
            GameSaveRow row = t.GetComponent<GameSaveRow>();

            row.selected = selectedSave == row.saveNameText.text;
        }
    }

    public void ChangeWindow(string window)
    {
        playWindow.SetActive(false);
        newGameWindow.SetActive(false);
        loadGameWindow.SetActive(false);
        settingsWindow.SetActive(false);

        switch (window)
        {
            case "Main":
                break;

            case "Play":
                playWindow.SetActive(true);
                break;

            case "NewGame":
                newGameWindow.SetActive(true);
                break;

            case "LoadGame":
                loadGameWindow.SetActive(true);
                break;

            case "Settings":
                settingsWindow.SetActive(true);
                break;
        }
    }

    public void NewGame()
    {
        if (!SavingSystem.FileExists($"Saves/{gameNameInput.text}.factory") && !string.IsNullOrEmpty(gameNameInput.text))
        {
            LoadGame(gameNameInput.text);
        }
    }

    public void LoadSelectedSave()
    {
        if (!string.IsNullOrEmpty(selectedSave))
        {
            LoadGame(selectedSave);
        }
    }

    private void LoadGame(string saveName)
    {
        BuildingManager.currentSaveName = saveName;

        SceneManager.LoadScene(gameSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LoadGameSaves()
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

    public void SelectSave(GameSaveRow saveRow)
    {
        selectedSave = saveRow.saveNameText.text;
    }
}
