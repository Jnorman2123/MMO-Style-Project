using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Declare variables for move input, strafe input, turn input, and mouse x input
    private float moveInput;
    private float turnInput;
    private float strafeInput;
    private float mouseXInput;
    // Declare variables for move speed and turn speed
    private float moveSpeed = 15f;
    private float turnSpeed;
    // Declare variables for turn direction, move direction, and strafe direction
    private float turnDirection;
    private Vector3 moveDirection;
    private Vector3 strafeDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the Move method
        Move();
        // Call the turn method
        Turn();
        // Call the Strafe method
        Strafe();
        // Call the MouseMove method
        MouseMove();
    }
    // Method to move the character based on vertical input
    private void Move()
    {
        // Set the moveInput variable to the vertical axis input
        moveInput = Input.GetAxisRaw("Vertical");
        // Check to see if the left and right buttons are both not being held
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
        {
            // Set the moveDirection to not move
            moveDirection = new Vector3(0.0f, 0.0f, 0.0f);
        } else
        {
            // Set the moveDirection based on the moveInput
            moveDirection = new Vector3(0.0f, 0.0f, moveInput);           
        }
        // Translate the player based on moveDirection
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
    // Method to strafe the character based on horizontal input when the right mouse is held down
    private void Strafe()
    {
        // Set the turnInput variable to the horizontal axis input
        strafeInput = Input.GetAxisRaw("Horizontal");
        // Set the strafeDirection based on the turnInput
        strafeDirection = new Vector3(strafeInput, 0.0f, 0.0f);
        // Check if the right mouse button is clicked and make character strafe based on strafeInput 
        if (Input.GetMouseButton(1))
        {
            // Translate the player side to side based on strafeDirection
            transform.Translate(strafeDirection * moveSpeed * Time.deltaTime);
        }
    }
    // Method to turn the character based on horizontal input
    private void Turn()
    {
        // Set the turnInput variable to the horizontal axis input
        turnInput = Input.GetAxisRaw("Horizontal");
        // Set the mouseXInput variable to the mouse X axis
        mouseXInput = Input.GetAxis("Mouse X");
        // Check if the right mouse button is being held and set the turnDirection and turnSpeed accordingly
        if (Input.GetMouseButton(1))
        {
            // Set the turnSpeed
            turnSpeed = 150f;
            // Set the turnDirection based on the mouseXInput
            turnDirection += mouseXInput * turnSpeed * Time.deltaTime;            
        } else
        {
            // Set the turnSpeed
            turnSpeed = 75f;
            // Set the turnDirection based on the turnInput and turn speed
            turnDirection += turnInput * turnSpeed * Time.deltaTime;
        }
        // Turn the player based on turnDirection
        transform.eulerAngles = new Vector3(0.0f, turnDirection, 0.0f);
    }
    // Method to make the character move forward when both the right and left mouse buttons are being held
    private void MouseMove()
    {
        // Check if both the right and left mouse buttons are being held and set the move direction to forward
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
        {
            // Set move direction to move forward
            moveDirection = new Vector3(0.0f, 0.0f, 1.0f);
            // Translate the player based on moveDirection
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }

    }
    // Method to make the character jump when space bar is pressed
    
}
