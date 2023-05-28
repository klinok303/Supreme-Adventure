using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;

    public Transform combatLookAt;

    public GameObject thirdPersonCam;
    public GameObject combatCam;
    public GameObject topDownCam;
    public ParticleSystem easterEggObj;

    public KeyCode switchOrientation;
    public KeyCode switchOrientation2;

    public CameraStyle currentStyle;
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    ReloadState rs;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rs = playerObj.gameObject.GetComponent<ReloadState>();
    }

    private void Update()
    {
        // switch styles
        if (Input.GetKeyDown(switchOrientation) && !Input.GetKeyDown(switchOrientation2))
        {
            if (currentStyle == CameraStyle.Basic) SwitchCameraStyle(CameraStyle.Combat);
            else if (currentStyle == CameraStyle.Combat) SwitchCameraStyle(CameraStyle.Basic);
        }
        if (Input.GetKeyDown(switchOrientation2)) SwitchCameraStyle(CameraStyle.Topdown);
        if (Input.GetKeyUp(switchOrientation2)) SwitchCameraStyle(CameraStyle.Basic);

        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        if(currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
            {
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                
                if (rs.GetStateID() == 1) Invoke("ActiveEasterEgg", 5.0f);
            }
            else if (easterEggObj.isPlaying) easterEggObj.Stop();
        }

        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
    }

    private void SwitchCameraStyle(CameraStyle newStyle)
    {
        combatCam.SetActive(false);
        thirdPersonCam.SetActive(false);
        topDownCam.SetActive(false);

        if (newStyle == CameraStyle.Basic) thirdPersonCam.SetActive(true);
        if (newStyle == CameraStyle.Combat) combatCam.SetActive(true);
        if (newStyle == CameraStyle.Topdown) topDownCam.SetActive(true);

        currentStyle = newStyle;
    }

    void ActiveEasterEgg()
    {
        if(easterEggObj.isPaused || easterEggObj.isStopped) easterEggObj.Play();
    }
}
