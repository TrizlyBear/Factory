using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private Vector3 startRotation;

    [Range(0f, 180f)] public float maxHorizontalSwayAngle = 10f;
    [Range(0f, 180f)] public float maxVerticalSwayAngle = 10f;

    public bool clampAngle = false;

    private void Start()
    {
        startRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        Vector3 pos = Input.mousePosition;

        float mouseX = pos.x.Remap(0f, Screen.currentResolution.width, -maxHorizontalSwayAngle, maxHorizontalSwayAngle, clampAngle);
        float mouseY = pos.y.Remap(0f, Screen.currentResolution.height, -maxVerticalSwayAngle, maxVerticalSwayAngle, clampAngle);

        Vector3 angles = startRotation + new Vector3(-mouseY, mouseX, 0);

        transform.localEulerAngles = angles;
    }
}
