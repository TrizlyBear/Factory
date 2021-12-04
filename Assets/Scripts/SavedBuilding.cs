using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedBuilding
{
    public SavedBuilding(string buildingType, Vector3 pos, int rotation)
    {
        this.buildingType = buildingType;

        position[0] = pos.x;
        position[1] = pos.y;
        position[2] = pos.z;

        this.rotation = rotation;
    }

    public string buildingType;

    public float[] position = new float[3];
    public int rotation;
}
