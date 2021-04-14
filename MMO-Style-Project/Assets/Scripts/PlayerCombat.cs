using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Declare variables for attack delay and attack damage
    private float attackDelay = 1.0f;
    private int attackDamage = 50;
    // Declare variable for the player target
    private GameObject playerTarget;
    // Declare variables for enemy max health and enemy current health
    private int enemyMaxHealth;
    private int enemyCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Call the AttackTarget method
        AttackTarget();
    }
    // Create method to attack the playerTarget if it is an enemy
    private void AttackTarget()
    {
        // Set the playerTarget to the game object benig targeted
        playerTarget = GetComponent<PlayerTargeting>().target;
        
        if (playerTarget.CompareTag("Enemy") & Input.GetKeyDown("`"))
        {
            // Call the enemy TakeDamage method with the damage 
            playerTarget.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }
}
