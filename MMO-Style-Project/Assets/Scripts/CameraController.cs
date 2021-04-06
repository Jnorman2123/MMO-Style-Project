using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Declare a variable to determine if in first or third person
    public bool firstPerson;
    // Declare variable for the first person camera game object
    public Transform firstPersonCamera;
    // Declare variable for mouse scroll wheel input
    private float mouseScrollWheel;
    // Declare variable for camera move speed
    private float cameraMoveSpeed;
    // Declare variable for camera offset
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraOffset = new Vector3(0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    // Use a late update
    private void LateUpdate()
    {
        // Call ZoomCamera method
        ZoomCamera();
        // Call the FollowPlayer method
        FollowPlayer();
        // Call the IsFirstPerson method
        IsFirstPerson();
    }
    // Method to zoom the camera in and out with the mouse wheel
    private void ZoomCamera()
    {
        // Set the cameraMoveSpeed
        cameraMoveSpeed = 10.0f;
        // When scrolling the mouse wheel zoom the camera from first person to third person
        mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        // Set camera offset move
        Vector3 offsetMove = new Vector3(0, 0, mouseScrollWheel);
        // Make camera look the firstPersonCamera object
        transform.LookAt(firstPersonCamera);
        // Move the camera in and out with the mouse wheel
        cameraOffset += offsetMove;
    }
    // Method to turn the camera
    private void TurnCamera()
    {

    }
    // Method to follow the player with camera
    private void FollowPlayer()
    {
        // Set the position of the camera to an offset of the player position
        transform.position = firstPersonCamera.position + cameraOffset;
        // Set the rotation of the camera to equal the rotation of the player
        transform.rotation = firstPersonCamera.rotation;
    }
    // Method to change firstPerson to true or false based on camera position
    private void IsFirstPerson()
    {
        if (cameraOffset == new Vector3(0,0,0))
        {
            firstPerson = true;
        } else
        {
            firstPerson = false;
        }
    }
}
