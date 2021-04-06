using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Declare and set variables movementSpeed and turnSpeed and turnPlayer
    private float movementSpeed = 10.0f;
    private float turnSpeed;
    private float turnPlayer = 0.0f;
    // Declare variable for jump force
    public float jumpForce;
    // Declare variables for player input
    private float verticalInput;
    private float horizontalInput;
    private float horizontalMouse;
    // Declare variable for rigidbody
    private Rigidbody objectRB;
    // Start is called before the first frame update
    void Start()
    {
        // Set rigidbody variable
        objectRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the Move method
        Move();
        // Check if player and space bar is pressed and then call Jump method
        if (gameObject.CompareTag("Player") & Input.GetKeyDown("space")) {
            Jump();
        }
    }

    // Create method to move the player based on the key pressed
    private void Move()
    {
        // Set inputs to vertical and horizontal input
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Set mouse axis input
        horizontalMouse = Input.GetAxisRaw("Mouse X");
        // Set movement based on object
        if (gameObject.CompareTag("Player"))
        {
            // Set vertical and horizontal movement
            Vector3 verticalMovement = new Vector3(0.0f, 0.0f, verticalInput);
            Vector3 horizontalMovement = new Vector3(horizontalInput, 0.0f, 0.0f);
            // Set to turn by mouse or horizontal key input
            float mouseTurn = horizontalMouse * turnSpeed * Time.deltaTime;
            float keyTurn = horizontalInput * turnSpeed * Time.deltaTime;
            // Set player turn vector
            Vector3 playerTurnVector = new Vector3(0.0f, turnPlayer, 0.0f);
            // Check which mouse button is pushed down to determine type of movement with horizontal input
            if (Input.GetMouseButton(0) & Input.GetMouseButton(1))
            {
                // Set foward movement of player
                Vector3 moveForward = new Vector3(0.0f, 0.0f, 1);
                // Set turnSpeed
                turnSpeed = 300.0f;
                // Move player forward
                transform.Translate(moveForward * Time.deltaTime * movementSpeed);
                // Turn player based on horizontal mouse input
                turnPlayer += mouseTurn;
                transform.eulerAngles = playerTurnVector;
                // Strafe charace side to side based on input
                transform.Translate(horizontalMovement * Time.deltaTime * movementSpeed);
            }
            if (Input.GetMouseButton(1) & !Input.GetMouseButton(0))
            {
                // Set turnSpeed
                turnSpeed = 300.0f;
                // Move character based on input
                transform.Translate(verticalMovement * Time.deltaTime * movementSpeed);
                // Turn player based on horizontal mouse input
                turnPlayer += mouseTurn;
                transform.eulerAngles = playerTurnVector;
                // Strafe charace side to side based on input
                transform.Translate(horizontalMovement * Time.deltaTime * movementSpeed);
            } 
            if (!Input.GetMouseButton(1)) 
            {
                // Set turnSpeed
                turnSpeed = 100.0f;
                // Move character based on input
                transform.Translate(verticalMovement * Time.deltaTime * movementSpeed);
                // Turn character based on input
                turnPlayer += keyTurn;
                transform.eulerAngles = playerTurnVector;
            }   
        }
    }

    // Create method to make the character jump when space is pressed
    private void Jump()
    {
        Debug.Log("jump");
        objectRB.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}
