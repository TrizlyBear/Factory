using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public PlayerMain player;

    [Space]

    [Header("HUD")]
    public TextMeshProUGUI buildingModeText;

    [Header("Building Mode")]
    public GameObject buildingModeUI;
    public TextMeshProUGUI buildingTypeText;

    [Header("Destroy Mode")]
    public GameObject destroyModeUI;
    public TextMeshProUGUI targetBuildingTypeText;

    public void SetBuildingMode(BuildingMode mode)
    {
        switch (mode)
        {
            case BuildingMode.None:
                buildingModeUI.SetActive(false);
                destroyModeUI.SetActive(false);
                buildingModeText.text = "";
                break;

            case BuildingMode.Building:
                buildingModeUI.SetActive(true);
                destroyModeUI.SetActive(false);
                buildingModeText.text = "Building Mode";
                break;

            case BuildingMode.Destroying:
                buildingModeUI.SetActive(false);
                destroyModeUI.SetActive(true);
                buildingModeText.text = "Destroy Mode";
                break;

            default:
                buildingModeUI.SetActive(false);
                destroyModeUI.SetActive(false);
                buildingModeText.text = "";
                break;
        }
    }
}
