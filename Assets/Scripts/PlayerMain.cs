using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMain : MonoBehaviour
{
    public PlayerMovement movementModule;
    public PlayerMouseLook mouseLookModule;
    public PlayerBuilding buildingModule;
    public PlayerAnimation animationModule;
    public PlayerUI uiModule;

    public UnityEvent<BuildingMode> onBuildingModeChanged = new UnityEvent<BuildingMode>();
}
