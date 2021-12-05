using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public enum BuildingMode
{
    None,
    Building,
    Destroying
}

public class PlayerBuilding : MonoBehaviour
{
    public PlayerMain player;

    [Space]

    public BuildingMode currentBuildingMode = BuildingMode.None;

    public bool buildingModeEnabled = false;
    public bool destroyModeEnabled = false;

    [Space]

    public Transform buildingsParent;
    public Transform cam;
    public LayerMask buildableLayers;
    public float range = 10f;
    public float gridSize = 0.25f;

    [Space]

    public Material hologramMaterial;
    public Color validPositionColor;
    public Color invalidPositionColor;

    private int selectedBuildingIndex = 0;
    public BuildingAsset selectedBuilding = null;

    [Header("Outline")]
    public Color destroyOutlineColor = Color.white;
    public float destroyOutlineWidth = 10f;

    public UnityEvent<Building> onBuildingPlaced = new UnityEvent<Building>();
    public UnityEvent<Building> onBuildingDestroyed = new UnityEvent<Building>();

    private BuildingMode previousBuildingMode;

    private GameObject currentHologram = null;
    private int currentRotation = 0;

    private GameObject currentDestroyTarget = null;

    private void Start()
    {
        selectedBuildingIndex = 0;
    }

    private void Update()
    {
        if (previousBuildingMode != currentBuildingMode)
        {
            player.onBuildingModeChanged.Invoke(currentBuildingMode);
            previousBuildingMode = currentBuildingMode;
        }

        if (BuildingManager.Instance.buildingTypes.Count != 0)
            selectedBuilding = BuildingManager.Instance.buildingTypes[selectedBuildingIndex];

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentBuildingMode == BuildingMode.Building)
                currentBuildingMode = BuildingMode.None;

            else
                currentBuildingMode = BuildingMode.Building;

            currentRotation = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentBuildingMode == BuildingMode.Destroying)
                currentBuildingMode = BuildingMode.None;

            else
                currentBuildingMode = BuildingMode.Destroying;
        }

        if (currentBuildingMode == BuildingMode.Building)
        {
            player.uiModule.buildingTypeText.text = selectedBuilding.buildingName;

            float scrollDelta = Input.mouseScrollDelta.y;

            if (scrollDelta != 0)
            {
                if (scrollDelta > 0)
                {
                    if (selectedBuildingIndex == BuildingManager.Instance.buildingTypes.Count - 1)
                        selectedBuildingIndex = 0;

                    else
                        selectedBuildingIndex++;
                }
                else
                {
                    if (selectedBuildingIndex == 0)
                        selectedBuildingIndex = BuildingManager.Instance.buildingTypes.Count - 1;

                    else
                        selectedBuildingIndex--;
                }

                if (currentHologram != null)
                    Destroy(currentHologram);
            }

            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, range, buildableLayers))
            {
                if (currentHologram == null)
                {
                    currentHologram = Instantiate(selectedBuilding.buildingHologramPrefab, hit.point, Quaternion.Euler(0, currentRotation, 0));
                }

                Vector3 holoPos = hit.point;
                holoPos.x = Mathf.Round(holoPos.x / gridSize) * gridSize;
                holoPos.y = Mathf.Round(holoPos.y / gridSize) * gridSize;
                holoPos.z = Mathf.Round(holoPos.z / gridSize) * gridSize;

                currentHologram.transform.position = holoPos;
                currentHologram.transform.rotation = Quaternion.Euler(0, currentRotation, 0);

                BuildingHologram holo = currentHologram.GetComponent<BuildingHologram>();

                if (!holo.IsColliding)
                {
                    hologramMaterial.color = validPositionColor;
                }
                else
                {
                    hologramMaterial.color = invalidPositionColor;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentRotation += 90;
                }

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (!holo.IsColliding)
                    {
                        Building building = Instantiate(selectedBuilding.buildingPrefab, holoPos, Quaternion.Euler(0, currentRotation, 0)).GetComponent<Building>();
                        building.transform.parent = buildingsParent;

                        onBuildingPlaced.Invoke(building);
                    }
                }
            }
            else
            {
                if (currentHologram != null)
                    Destroy(currentHologram);
            }
        }
        else
        {
            if (currentHologram != null)
                Destroy(currentHologram);
        }

        if (currentBuildingMode == BuildingMode.Destroying)
        {
            if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hit, range))
            {
                GameObject obj = hit.collider.gameObject;

                if (obj.CompareTag("Building"))
                {
                    if (obj != currentDestroyTarget)
                        if (currentDestroyTarget != null)
                            SetOutline(currentDestroyTarget, false);

                    currentDestroyTarget = obj;

                    player.uiModule.targetBuildingTypeText.text = obj.GetComponent<Building>().buildingType.buildingName;

                    SetOutline(obj, true);
                }
                else
                {
                    if (currentDestroyTarget != null)
                        SetOutline(currentDestroyTarget, false);

                    currentDestroyTarget = null;

                    player.uiModule.targetBuildingTypeText.text = "";
                }
            }
            else
            {
                if (currentDestroyTarget != null)
                    SetOutline(currentDestroyTarget, false);

                currentDestroyTarget = null;

                player.uiModule.targetBuildingTypeText.text = "";
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (currentDestroyTarget != null)
                {
                    onBuildingDestroyed.Invoke(currentDestroyTarget.GetComponent<Building>());

                    Destroy(currentDestroyTarget);
                }
            }
        }
        else
        {
            if (currentDestroyTarget != null)
                SetOutline(currentDestroyTarget, false);

            currentDestroyTarget = null;
        }
    }

    private void SetOutline(GameObject obj, bool setOutline)
    {
        Outline outline = obj.GetComponent<Outline>();

        if (setOutline)
        {
            if (outline == null)
            {
                Outline ol = obj.AddComponent<Outline>();

                ol.OutlineWidth = destroyOutlineWidth;
                ol.OutlineColor = destroyOutlineColor;
            }
        }
        else
        {
            if (outline != null)
            {
                Destroy(obj.GetComponent<Outline>());
            }
        }
    }
}
