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
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    //get mouseX input
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    //get mouseY input

        xRotation -= mouseY;                                                            //xRotation changes from mouseX input
        yRotation += mouseX;                                                            //yRotation changes from mouseY input

        xRotation = Mathf.Clamp(xRotation, -rotationOffset, rotationOffset);            //xRotation can be changed between positive and negative rotationOffset
        yRotation = Mathf.Clamp(yRotation, -rotationOffset, rotationOffset);            //yRotation can be changed between positive and negative rotationOffset

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);           //change camera rotation using the mouse
    }
}
