using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCamera : MonoBehaviour
{
    public Transform cam;

    void FixedUpdate() => transform.rotation = cam.rotation *  Quaternion.AngleAxis(5, Vector3.up);
}
