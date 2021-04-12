using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    // Declare variables for the first and third person cameras
    public CinemachineVirtualCamera firstPersonCamera;
    public CinemachineFreeLook thirdPersonCamera;
    // Declare variable for first person
    private bool firstPerson;
    // Start is called before the first frame update
    void Start()
    {
        // Set firstPerson to true at start
        firstPerson = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SwitchCamera method
        SwitchCamera();
    }
    // Create a method to switch the active camera
    private void SwitchCamera()
    {
        if(Input.GetKeyDown(KeyCode.F9))
        {
            firstPerson = !firstPerson;
        }
        // Set the priorty of each camera based on firstPerson or not
        if(firstPerson)
        {
            firstPersonCamera.Priority = 1;
            thirdPersonCamera.Priority = 0;
        } else
        {
            firstPersonCamera.Priority = 0;
            thirdPersonCamera.Priority = 1;
        }
    }
}
