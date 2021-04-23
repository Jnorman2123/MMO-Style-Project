using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Declare variable for move speed
    private float moveSpeed;
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
        // Set the initial moveSpeed to walking speed
        moveSpeed = 2.5f;
        // Set the player game object
        player = GameObject.Find("Zidgog");
    }

    // Update is called once per frame
    void Update()
    {
        
        // Call the ChaseTarget method if the enemy has hate towards the player
        if (GetComponent<EnemyController>().isAggro)
        {
            ChaseTarget();
        } else
        {
            // Call the Patrol method
            Patrol();
        }
    }
    // Create method to move the character if he is a patrol
    private void Patrol()
    {
        // Set the turnDirection
        turnDirection = new Vector3(0.0f, 180.0f, 0.0f);
        // If the enemy is a patrol begin to move the character forward slowly
        if (gameObject.GetComponent<EnemyController>().isPatrol == true)
        {
            enemyCharacterController.Move(transform.forward * moveSpeed * Time.deltaTime);
            // If the enemy reaches the end points of its route it will stop, turn around and then begin moving again after a few seconds
            if ((transform.position.x <= -15 & moveSpeed > 0.0f) || (transform.position.x >= 15 & moveSpeed > 0.0f))
            {
                moveSpeed = 0.0f;
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
        moveSpeed = 2.5f;
    }
    // Create method to make the enemy chase the target if he is aggro 
    private void ChaseTarget()
    {
        // Set the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.transform.position);
        // Set the moveSpeed based on if the enemy is in attack range
        if (distance <= GetComponent<EnemyCombat>().attackRange)
        {
            moveSpeed = 0.0f;
        } else
        {
            // Set the MoveSpeed
            moveSpeed = 8.0f;
        }
        // Have the enemy face the player and then move forward
        transform.LookAt(player.transform);
        enemyCharacterController.Move(transform.forward * moveSpeed * Time.deltaTime);
    }
}
