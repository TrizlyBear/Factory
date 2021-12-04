using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    public int resolution = 0;
    public int windowMode = 0;
    public int fpsLimit = 0;
    public bool vSync = true;

    public bool invertMouseY = false;
    public float sensitivity = 5f;

    public float masterVolume = 0f;
    public float sfxVolume = 0f;
    public float musicVolume = 0f;

    public bool mapLockedNorth = true;
}

public class GameSettings : MonoBehaviour
{
    public Settings currentSettings = new Settings();

    [Header("UI Elements")]
    public OptionCycle resolutionSetting;
    public OptionCycle windowModeSetting;
    public OptionCycle fpsLimitSetting;
    public OptionCycle vSyncSetting;

    public OptionCycle invertMouseYSetting;
    public Slider sensitivitySetting;

    public Slider masterVolumeSetting;
    public Slider sfxVolumeSetting;
    public Slider musicVolumeSetting;

    public OptionCycle mapLockedNorthSetting;

    private Resolution[] resolutions;

    private void Start()
    {
        LoadSettings();

        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        foreach (Resolution res in resolutions)
        { 
            options.Add(res.Formatted());
        }

        resolutionSetting.options = options;
    }

    public void UpdateSettingsWindow()
    {
        resolutionSetting.SetOption(currentSettings.resolution);
        windowModeSetting.SetOption(currentSettings.windowMode);
        fpsLimitSetting.SetOption(currentSettings.fpsLimit);
        vSyncSetting.SetOption(currentSettings.vSync ? 0 : 1);
        invertMouseYSetting.SetOption(currentSettings.invertMouseY ? 0 : 1);
        sensitivitySetting.slider.value = currentSettings.sensitivity;
        masterVolumeSetting.slider.value = currentSettings.masterVolume.Remap(-80f, 20f, 0f, 10f, true);
        sfxVolumeSetting.slider.value = currentSettings.sfxVolume.Remap(-80f, 20f, 0f, 10f, true);
        musicVolumeSetting.slider.value = currentSettings.musicVolume.Remap(-80f, 20f, 0f, 10f, true);
        mapLockedNorthSetting.SetOption(currentSettings.mapLockedNorth ? 0 : 1);
    }

    public void SaveSettings()
    {
        SavingSystem.SaveData("gameSettings.settings", currentSettings);
    }

    public void LoadSettings()
    {
        if (SavingSystem.FileExists("gameSettings.settings"))
        {
            currentSettings = SavingSystem.LoadData<Settings>("gameSettings.settings");
        }

        UpdateSettingsWindow();
    }

    #region Settings

    public void SetResolution(int index)
    {
        currentSettings.resolution = index;

        SaveSettings();
    }

    public void SetWindowMode(int mode)
    {
        currentSettings.windowMode = mode;

        SaveSettings();
    }

    public void SetFpsLimit(int index)
    {
        currentSettings.fpsLimit = index;

        SaveSettings();
    }

    public void SetVsync(int i)
    {
        currentSettings.vSync = i == 0;

        SaveSettings();
    }

    public void SetInvertedMouse(int i)
    {
        currentSettings.invertMouseY = i == 0;

        SaveSettings();
    }

    public void SetSensitivity(float sens)
    {
        currentSettings.sensitivity = sens;

        SaveSettings();
    }

    public void SetMapLockedNorth(int i)
    {
        currentSettings.mapLockedNorth = i == 0;

        SaveSettings();
    }

    public void SetMasterVolume(float volume)
    {
        currentSettings.masterVolume = volume.Remap(0f, 10f, -80f, 20f, true);
    }

    public void SetSfxVolume(float volume)
    {
        currentSettings.sfxVolume = volume.Remap(0f, 10f, -80f, 20f, true);
    }

    public void SetMusicVolume(float volume)
    {
        currentSettings.musicVolume = volume.Remap(0f, 10f, -80f, 20f, true);
    }

    #endregion
}
