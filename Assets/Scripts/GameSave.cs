using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave
{
    public GameSave(string gameName, List<SavedBuilding> buildings = null)
    {
        GameName = gameName;

        if (buildings != null)
            Buildings = buildings.ToArray();

        else
            Buildings = new SavedBuilding[0];

        saveTime = DateTime.Now.ToString("H:mm");
        saveDate = DateTime.Now.ToString("D");
    }

    public string GameName { get; }

    public string saveTime { get; }
    public string saveDate { get; }

    public SavedBuilding[] Buildings { get; }

    public override string ToString()
    {
        return $"{GameName} - {saveTime} - {saveDate}";
    }
}
