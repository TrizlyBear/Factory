using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Building : MonoBehaviour
{
    public BuildingAsset buildingType;

    private protected void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y = Mathf.Round(rot.y / 90f) * 90f;
        transform.eulerAngles = rot;
    }

    public SavedBuilding GetSerializedBuilding()
    {
        return new SavedBuilding(buildingType.buildingName, transform.position, (int)transform.eulerAngles.y);
    }
}
