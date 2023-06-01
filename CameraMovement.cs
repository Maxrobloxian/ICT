using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Pohyb camery a rotace hrace
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;

    float xRotation;
    float yRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        player.transform.rotation = orientation.rotation;

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.position = new Vector3(orientation.position.x, orientation.position.y + .86f, orientation.position.z);
    }
}