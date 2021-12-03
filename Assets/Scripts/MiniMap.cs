using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public bool mapEnabled = true;

    [Space]

    public Camera mapCamera;

    public PlayerMain player;
    public RectTransform playerMarker;

    [Header("Settings")]
    [Range(1, 20)] public int zoomLevel = 10;
    public bool alwaysFaceNorth = true;

    private void Update()
    {
        mapCamera.orthographicSize = zoomLevel;

        Vector3 playerPos = player.transform.position;
        playerPos.y = mapCamera.transform.position.y;
        mapCamera.transform.position = playerPos;

        if (alwaysFaceNorth)
        {
            mapCamera.transform.eulerAngles = new Vector3(90f, 0f, 0f);
        }
        else
        {
            mapCamera.transform.eulerAngles = new Vector3(90f, player.transform.eulerAngles.y, 0f);
        }
    }
}
