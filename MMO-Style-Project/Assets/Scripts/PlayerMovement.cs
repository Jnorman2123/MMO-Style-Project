using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Declare variable for the character controller
    public CharacterController playerController;
    // Declare variables for move speed and turn speed
    private float moveSpeed = 10.0f;
    private float turnSpeed = 150.0f;
    // Declare variables for move, turn and strafe inputs
    private float moveInput;
    private float turnInput;
    private float strafeInput;
    // Declare variables for moveDirection, strafeDirection and turnDirection
    private Vector3 moveDirection;
    private Vector3 strafeDirection;
    private Vector3 turnDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    // Create method Move to hold all the player movement methods
    private void Move()
    {
        // Call the MouseMove method
        MouseMove();
        // Call the Strafe method
        Strafe();
        // Call the Move method
        KeyMove();
        // Call the Turn method
        Turn();
        // Call the MouseTurn method
        MouseTurn();
    }
    // Create method for moving the player
    private void KeyMove()
    {
        // Set the moveInput to the vertical axis input
        moveInput = Input.GetAxisRaw("Vertical");
        // Check to see if both the right and left mouse buttons are being held down and changed the moveSpeed
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
        {
            moveDirection = Vector3.zero;
        } else
        {
            // Set the moveDirection to a forward Vector3 and moveInput
            moveDirection = transform.forward * moveInput;
        }  
        // Move the player based on the moveDirection and moveSpeed
        playerController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
    // Create method for turning the player
    private void Turn()
    {
        // Check to see if the right mouse is not being held
        if (!Input.GetMouseButton(1))
        {
            // Set the turnInput to the horizontal axis input
            turnInput = Input.GetAxisRaw("Horizontal");
            // Set the turnDirection based on the turnInput 
            turnDirection = new Vector3(0.0f, turnInput * turnSpeed * Time.deltaTime, 0.0f);
            // Rotate the player based on the turnDirection
            transform.Rotate(turnDirection);
        }
    }
    // Create a method for making the player strafe side to side when the right mouse is being held
    private void Strafe()
    {
        // Set the strafeInput to the horizontal axis input
        strafeInput = Input.GetAxisRaw("Horizontal");
        // Set the strafeDirection based on the strafeInput
        strafeDirection = transform.right * strafeInput;
        // Check to see if the right mouse button is being held
        if (Input.GetMouseButton(1))
        {
            // Move the player based on the strafeDirection and moveSpeed
            playerController.Move(strafeDirection * moveSpeed * Time.deltaTime);
        }
    }
    // Create a method to move forward when both the left and right mouse buttons are being held
    private void MouseMove()
    {
        // Check to see if both the right and left mouse buttons are being held
        if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
        {
            // Set the moveDirection to a forward Vector3 and moveInput
        moveDirection = transform.forward;
        // Move the player based on the moveDirection and moveSpeed
        playerController.Move(moveDirection * moveSpeed * Time.deltaTime);
        } 
    }
    // Create a method to turn the character when the right mouse is being held
    private void MouseTurn()
    {
        // Check to see if the right mouse button is being held
        if (Input.GetMouseButton(1))
        {
            // Set the turnInput to the horizontal axis input
            turnInput = Input.GetAxis("Mouse X");
            // Set the turnDirection based on the turnInput 
            turnDirection = new Vector3(0.0f, turnInput * turnSpeed * Time.deltaTime, 0.0f);
            // Rotate the player based on the turnDirection
            transform.Rotate(turnDirection);
        }       
    }
}
