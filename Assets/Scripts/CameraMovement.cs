using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float yClampRotation = 180.0f; // Maximum allowed Y rotation
    public float xClampRotation = 180.0f; // Maximum allowed x rotation

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        xRotation += mouseX;
        yRotation -= mouseY;

        // Clamp X rotation to prevent looking unnaturally
        xRotation = Mathf.Clamp(xRotation, -xClampRotation, xClampRotation);
        // Clamp Y rotation to prevent looking upside down
        yRotation = Mathf.Clamp(yRotation, -yClampRotation, yClampRotation);

        // Apply rotations
        transform.localRotation = Quaternion.Euler(yRotation, xRotation, 0.0f);
    }
}
