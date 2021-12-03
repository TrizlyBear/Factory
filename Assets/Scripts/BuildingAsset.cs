using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Factory/Building", order = 0)]
public class BuildingAsset : ScriptableObject
{
    public string buildingName;

    public GameObject buildingHologramPrefab;
    public GameObject buildingPrefab;
}
