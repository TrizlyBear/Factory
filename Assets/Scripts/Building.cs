using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingAsset buildingType;

    public SavedBuilding GetSerializedBuilding()
    {
        return new SavedBuilding(buildingType.buildingName, transform.position, (int)transform.eulerAngles.y);
    }
}
