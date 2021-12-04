using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{
    public PlayerMain player;

    [Space]

    public Transform cam;

    [Header("Settings")]
    [Range(0f, 10f)] public float sensitivity = 5f;

    float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * (sensitivity * 100f) * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * (sensitivity * 100f) * Time.deltaTime;

        if (GameSettings.Instance.currentSettings.invertMouseY)
            mouseY = -mouseY;

            xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
