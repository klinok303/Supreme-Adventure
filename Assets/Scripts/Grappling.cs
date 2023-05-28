using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    public KeyCode FastKey;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;

    private float maxDistance = 100f;
    private Vector3 grapplePoint;
    private SpringJoint joint;

    LineRenderer lr;
    float distance;

    void Awake() => lr = GetComponent<LineRenderer>();

    void OnDisable() => StopGrapple();

    void Update() 
    {
        if (Input.GetMouseButtonDown(0)) StartGrapple();
        else if (Input.GetMouseButtonUp(0)) StopGrapple();
        else if (Input.GetMouseButton(0)) DistanceUpdate();
    }

    //Called after Update
    void LateUpdate() => DrawRope();

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() 
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) 
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            distance = distanceFromPoint;

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            lr.enabled = true;
            currentGrapplePosition = gunTip.position;
        }
    }

    void DistanceUpdate()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) joint.maxDistance = (distance + Input.GetAxis("Mouse ScrollWheel") * 10f) * 0.8f;
        if (Input.GetKey(FastKey)) joint.maxDistance = (distance - 1000f) * 0.8f; 
    }

    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() 
    {
        lr.positionCount = 0;
        lr.enabled = false;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    void DrawRope() 
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() 
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}