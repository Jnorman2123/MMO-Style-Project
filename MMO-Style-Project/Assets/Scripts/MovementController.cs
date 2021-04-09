using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Declare variables for move and turn input
    private float moveInput;
    private float turnInput;
    // Declare variables for move speed and turn speed
    public float moveSpeed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the Move method
        Move();
    }
    // Method to move the character based on vertical input
    private void Move()
    {
        // Set the moveInput variable to the vertical axis input
        moveInput = Input.GetAxisRaw("Vertical");
        // Set the direction to move based on the vertical input
        Vector3 moveDirection = new Vector3(0.0f, 0.0f, moveInput);
        // Translate the player based on move direction and move speed
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }
}
