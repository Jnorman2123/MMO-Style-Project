using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Declare variables for attack delay, attack damage, and attack range
    private float attackDelay = 2.0f;
    private int attackDamage = 50;
    private float attackRange = 10.0f;
    // Declare variable for the player target
    private GameObject playerTarget;
    // Declare variables for auto attacking and inAttackRange
    private bool autoAttacking;
    public bool inAttackRange;
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
        // Call the InAttackRange method
        InAttackRange();    
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
            } else
            {
                // Stop the Attack coroutine
                StopCoroutine("Attack");
            }
        }
    }
    // Create method to determine if the player is in attack range
    private void InAttackRange()
    {
        // Set the layer mask to the targetable layer only
        int layerMask = (1 << 8);
        // Set the playerTarget to the game object benig targeted
        playerTarget = GetComponent<PlayerTargeting>().target;
        if (playerTarget != null)
        {
            RaycastHit hit;
            // Use a raycast to determine if the player is in attack range of the target
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, layerMask))
            {
                // Create variable for the game object of the hit parent
                GameObject hitObject = hit.transform.parent.gameObject; 
                // Check if the hitObject is an enemy and the current target of the player then set inAttackRange
                if (hitObject.CompareTag("Enemy") & hitObject.name == playerTarget.name)
                {
                    inAttackRange = true;
                }      
            }   else
            {
                inAttackRange = false;
            }
        } else
        {
            inAttackRange = false;
        }
        
    }
    // Create IEnum to attack with attackDelay
    IEnumerator Attack()
    {
        // Set the playerTarget to the game object being targeted
        playerTarget = GetComponent<PlayerTargeting>().target;
        Debug.Log(playerTarget);
        // Repeat while autoAttacking is true
        while(autoAttacking)
        {
            // If the playerTarget is equal to null log "You have no target."
            if (playerTarget == null)
            {
                Debug.Log("You have no target.");
                yield return new WaitForSeconds(attackDelay);
            } else {
                // Use a switch case statement to take certain action based on the playerTarget tag
                string tag = playerTarget.gameObject.tag;
                switch (tag)
                {
                    case "Player":
                        Debug.Log("You can't attack yourself");
                        yield return new WaitForSeconds(attackDelay);
                        break;
                    case "Interactable":
                        Debug.Log("You cannot attack your current target.");
                        yield return new WaitForSeconds(attackDelay);
                        break;
                    case "Enemy":
                        if (!inAttackRange)
                        {
                            Debug.Log("You are too far away from your target.");
                            yield return new WaitForSeconds(attackDelay);
                        }
                        else
                        {
                            Debug.Log("Auto Attacking");
                            DamageTarget();
                            if (playerTarget.GetComponent<EnemyController>().currentHealth <= 0)
                            {
                                Debug.Log("Enemy defeated");
                                StopCoroutine("Attack");
                                playerTarget = null;
                                autoAttacking = false;
                            }
                            yield return new WaitForSeconds(attackDelay);
                        }
                        break;
                }
            }
        }
    }
}
