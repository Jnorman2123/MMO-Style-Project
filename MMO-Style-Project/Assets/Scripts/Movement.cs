using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Declare and set variables movementSpeed and turnSpeed and turnPlayer
    public float movementSpeed = 10.0f;
    public float turnSpeed = 20.0f;
    private float turnPlayer = 0.0f;
    // Declare variables for player input
    private float verticalInput;
    private float horizontalInput;
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
        Move();
    }

    // Create function to move the player based on the key pressed
    private void Move()
    {
        // Set inputs to vertical and horizontal input
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
        // Set movement based on object
        if (gameObject.CompareTag("Player"))
        {
            Vector3 movement = new Vector3(0.0f, 0.0f, verticalInput);
            // Move character based on input
            transform.Translate(movement * Time.deltaTime * movementSpeed);
            // Turn charatcter based on input
            turnPlayer += horizontalInput * turnSpeed * Time.deltaTime;
            transform.eulerAngles = new Vector3(0.0f, turnPlayer, 0.0f);
        }
    }
}
