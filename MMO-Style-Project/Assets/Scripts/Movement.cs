using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Declare variables movementSpeed and turnSpeed
    public float movementSpeed;
    public float turnSpeed;
    // Declare variables for player input
    private float verticalInput;
    private float horinzontalInput;
    // Start is called before the first frame update
    void Start()
    {
        // Set the movement speed based on the game object
        if (gameObject.CompareTag("Player"))
        {
            movementSpeed = 10.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Create function to move the player based on the key pressed
    private void Move(float movementSpeed)
    {
        // Move the player based on the key pressed
        if (Input.GetKeyDown(KeyCode.W) & gameObject.CompareTag("Player"))
        {
            gameObject.transform.forward
        }
    }
}
