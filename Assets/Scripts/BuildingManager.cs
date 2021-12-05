using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static string currentSaveName = "Test";

    public GameSave currentSaveFile = null;

    private static BuildingManager _instance = null;
    public static BuildingManager Instance { get { return _instance; } }

    public Transform buildingsParent;

    public List<BuildingAsset> buildingTypes = new List<BuildingAsset>();

    public List<Building> buildings = new List<Building>();

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
        LoadGame();
    }

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

    public void LoadGame()
    {
        if (SavingSystem.FileExists($"Saves/{currentSaveName}.factory"))
        {
            currentSaveFile = SavingSystem.LoadData<GameSave>($"Saves/{currentSaveName}.factory");
        }
        else
        {
            currentSaveFile = new GameSave(currentSaveName);
        }

        foreach (Building building in buildings)
        {
            Destroy(building.gameObject);
        }

        foreach (SavedBuilding building in currentSaveFile.Buildings)
        {
            foreach (BuildingAsset asset in buildingTypes)
            {
                if (building.buildingType == asset.buildingName)
                {
                    Vector3 pos = new Vector3(building.position[0], building.position[1], building.position[2]);

                    Building newBuilding = Instantiate(asset.buildingPrefab, pos, Quaternion.Euler(0f, building.rotation, 0f)).GetComponent<Building>();
                    newBuilding.transform.parent = buildingsParent;

                    buildings.Add(newBuilding);

                    break;
                }
            }
        }

        SaveGame();
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
