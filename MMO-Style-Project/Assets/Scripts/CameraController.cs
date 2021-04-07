using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Declare a variable to determine if in first or third person
    private bool firstPerson;
    // Declare variable for the first person camera game object
    public Transform firstPersonCamera;
    // Declare a variable to determine if the firstPersonCamera is moving
    public bool fpcMoving;
    // Declare variable for position of the first person camera
    private Vector3 fpcPos;
    // Declare variable for mouse scroll wheel input
    private float mouseScrollWheel;
    // Declare variable for camera move speed
    private float cameraMoveSpeed;
    // Declare variable for camera offset
    private Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        // Set the cameraOffset variable
        cameraOffset = new Vector3(0, 0, 0);
        // Set the initial position of the first person camera
        fpcPos = firstPersonCamera.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    // Use a late update
    private void LateUpdate()
    {
        // Call the IsFirstPerson method
        IsFirstPerson();
        // Call the IsFPCMoving method
        IsFPCMoving();
        // Call ZoomCamera method
        ZoomCamera();
        
        // If the firstPerson is true follow the player rotation else call TurnCamera
        if (firstPerson)
        {
            // Set the position of the camera to an offset of the player position
            transform.position = firstPersonCamera.position + cameraOffset;
            // Call the FollowPlayerRotation method
            FollowPlayerRotation();
        } else 
        {
            // Call the TurnCamer method
            TurnCamera();
        }
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
        // Compute the position the object will reach
        Vector3 desiredPosition = firstPersonCamera.rotation * (firstPersonCamera.position);

        // Compute the direction the object will look at
        Vector3 desiredDirection = Vector3.Project(firstPersonCamera.forward, (firstPersonCamera.position - desiredPosition).normalized);

        // Rotate the object
        transform.rotation = Quaternion.Slerp(firstPersonCamera.rotation, Quaternion.LookRotation(desiredDirection), Time.deltaTime * cameraMoveSpeed);

        // Place the object to "compensate" the rotation
        transform.position = firstPersonCamera.position - transform.forward * cameraOffset.magnitude;
    }
    // Method to follow the player rotation with camera
    private void FollowPlayerRotation()
    { 
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
    // Method to check if the first person camera is moving
    private void IsFPCMoving()
    {
        // Declare a variable to calculate first person camera movement
        Vector3 fpcMovement = firstPersonCamera.position - fpcPos;
        fpcPos = firstPersonCamera.position;

        if (fpcMovement.magnitude > 0.001f)
        {
            fpcMoving = true;
        } else
        {
            fpcMoving = false;
        }
    }
}
