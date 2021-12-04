using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameSave currentSaveFile =  new GameSave("Test");

    private static BuildingManager _instance = null;
    public static BuildingManager Instance { get { return _instance; } }

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

    public List<Building> buildings = new List<Building>();

    public void SaveGame()
    {
        List<SavedBuilding> savedBuildings = new List<SavedBuilding>();

        foreach (Building building in buildings)
        {
            savedBuildings.Add(building.GetSerializedBuilding());
        }

        currentSaveFile = new GameSave(currentSaveFile.GameName, savedBuildings);

        if (!SavingSystem.DirExists("Saves"))
            SavingSystem.CreateDir("Saves");

        SavingSystem.SaveData($"Saves/{currentSaveFile.GameName}.factory", currentSaveFile);
    }

    public void AddBuilding(Building building)
    {
        buildings.Add(building);

        SaveGame();
    }

    public void RemoveBuilding(Building building)
    {
        if (buildings.Contains(building))
            buildings.Remove(building);

        SaveGame();
    }
}
