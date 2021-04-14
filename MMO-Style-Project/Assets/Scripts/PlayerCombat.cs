using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Declare variables for attack delay and attack damage
    private float attackDelay = 2.0f;
    private int attackDamage = 50;
    // Declare variable for the player target
    private GameObject playerTarget;
    // Declare variable for auto attacking
    private bool autoAttacking;
    // Start is called before the first frame update
    void Start()
    {
        autoAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the AutoAttack method
        AutoAttack();
            
    }
    // Create method to attack the playerTarget if it is an enemy
    private void DamageTarget()
    {
        // Set the playerTarget to the game object benig targeted
        playerTarget = GetComponent<PlayerTargeting>().target;
        // Call the enemy TakeDamage method with the damage 
        playerTarget.GetComponent<EnemyController>().TakeDamage(attackDamage);
    }
    // Create method to toggle auto attack
    private void AutoAttack()
    {
        // If player is targeting an enemy and presses tilde toggle auto attack
        if (Input.GetKeyDown("`"))
        {
            autoAttacking = !autoAttacking;
            Debug.Log(autoAttacking);
            // Check to see if autoAttacking and start or stop the attack coroutine
            if (autoAttacking)
            {
                // Start the Attack coroutine
                StartCoroutine("Attack");
            }
        }
    }
    // Create IEnum to attack with attackDelay
    IEnumerator Attack()
    {
        // Set the playerTarget to the game object benig targeted
        playerTarget = GetComponent<PlayerTargeting>().target;
        // If autoAttacking call the DamageTarget method after attackDelay
        while (autoAttacking & playerTarget.GetComponent<EnemyController>().currentHealth > 0)
        {
            yield return new WaitForSeconds(attackDelay);
            if (playerTarget.CompareTag("Enemy")) 
            {
                Debug.Log("auto attacking");
                DamageTarget();               
            } else if (playerTarget.CompareTag("Player"))
            {
                Debug.Log("You can't attack yourself");
            } else if (playerTarget = null)
            {
                Debug.Log("You have no target");
            }
            
        }
    }
}
