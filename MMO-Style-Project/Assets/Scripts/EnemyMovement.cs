using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Declare variable for move speed and off path
    private float enemyMoveSpeed;
    private bool offPath;
    // Declare variable for gravity and fallDirection
    private float gravity = 10.0f;
    private Vector3 fallDirection;
    // Declare variables for spawn position and rotation
    private Vector3 spawnPos;
    private Quaternion spawnRot;
    // Declare variable for the character controller
    public CharacterController enemyCharacterController;
    // Declare variable for the player game object
    private GameObject player;
    // Declare variable for turn direction
    private Vector3 turnDirection;
    // Declare variable for stationary time
    private float stationaryTime = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        // Set enemy move speed to walk
        enemyMoveSpeed = 2.5f;
        // Set offPath
        offPath = false;
        // Set the spawn position and rotation
        spawnPos = transform.position;
        spawnRot = transform.rotation;
        // Set the player game object
        player = GameObject.Find("Zidgog");
    }

    // Update is called once per frame
    void Update()
    {
        // Call the Fall method
        Fall();
        // Call the ChaseTarget method if the enemy has hate towards the player
        if (GetComponent<EnemyController>().isAggro)
        {
            ChaseTarget();
        } else if (offPath == true & !GetComponent<EnemyController>().isAggro)
        {
            // Call the ReturnToSpawn method
            ReturnToSpawn();
        } else if (offPath == false & !GetComponent<EnemyController>().isAggro)
        {
            // Call the Patrol method
            Patrol();
        }
    }
    // Create method to move the character if he is a patrol
    private void Patrol()
    {
        // If the enemy is a patrol begin to move the character forward slowly
        if (GetComponent<EnemyController>().isPatrol == true)
        {
            // Set the turnDirection
            turnDirection = new Vector3(0.0f, 180.0f, 0.0f);
            enemyCharacterController.Move(transform.forward * enemyMoveSpeed * Time.deltaTime);
            // If the enemy reaches the end points of its route it will stop, turn around and then begin moving again after a few seconds
            if ((transform.position.x <= -15 & enemyMoveSpeed > 0.0f) || (transform.position.x >= 15 & enemyMoveSpeed > 0.0f))
            {
                enemyMoveSpeed = 0.0f;
                transform.Rotate(turnDirection);
                // Start the WaitToMove coroutine
                StartCoroutine("WaitToMove");
            }
        } 
    }
    // Create an ienumerator to wait a few seconds then move again
    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(stationaryTime);
        enemyMoveSpeed = 2.5f;
    }
    // Create method to make the enemy chase the target if he is aggro 
    private void ChaseTarget()
    {
        // Set the moveSpeed based on if the enemy is in attack range
        if (GetComponent<EnemyCombatController>().inAttackRange)
        {
            enemyMoveSpeed = 0.0f;
        } else
        {
            // Set the MoveSpeed
            enemyMoveSpeed = 8.0f;
        }
        // Have the enemy face the player and then move forward
        transform.LookAt(player.transform);
        enemyCharacterController.Move(transform.forward * enemyMoveSpeed * Time.deltaTime);
        offPath = true;
    }
    // Create a method to return to start position if no aggro
    private void ReturnToSpawn()
    {
        enemyMoveSpeed = 2.5f;
        transform.LookAt(spawnPos + new Vector3(0.0f, 0.1f, 0.0f));
        enemyCharacterController.Move(transform.forward * enemyMoveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, spawnPos) < 0.15f)
        {
            offPath = false;
            enemyCharacterController.transform.position = spawnPos;
            enemyCharacterController.transform.rotation = spawnRot;
            enemyMoveSpeed = 0.0f;
            if (GetComponent<EnemyController>().isPatrol == true)
            {
                StartCoroutine("WaitToMove");
            }
        }
    }
    // Create Fall method to simulate gravity
    private void Fall()
    {
        // Check to see if the player is on the ground
        if (!enemyCharacterController.isGrounded)
        {
            // Set the gravity
            gravity = -10.0f;
            // Set the fallDirection
            fallDirection = transform.up * gravity;
            // Move the player based on the fallDirection and gravity
            enemyCharacterController.Move(fallDirection * Time.deltaTime);
        }
    }
}
