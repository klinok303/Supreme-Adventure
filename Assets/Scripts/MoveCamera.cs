using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cam;

    void FixedUpdate() => transform.position = cam.position;
}
