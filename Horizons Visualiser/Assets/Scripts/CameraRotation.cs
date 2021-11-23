using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public float rotationOffset = 10f;

    float xRotation = 0f, yRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -rotationOffset, rotationOffset);
        yRotation = Mathf.Clamp(yRotation, -rotationOffset, rotationOffset);

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
