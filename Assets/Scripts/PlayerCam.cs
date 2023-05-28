using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sens;

    public Transform orientation;

    float rotateX;
    float rotateY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sens;

        rotateY += mouseX;

        rotateX -= mouseY;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(rotateX, rotateY, 0);
        orientation.rotation = Quaternion.Euler(0, rotateY, 0);
    }
}
