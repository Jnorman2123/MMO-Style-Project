using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    // Declare variables for attack damage, attack delay, and attack range
    public int enemyAttackDamage;
    public float enemyAttackDelay;
    public int enemyAttackRange;
    // Declare variable for the player game object
    private GameObject player;
    // Declare variable for chat window ui
    private GameObject chatUIWindow;
    // Declare variables for auto attacking, attackStarted,  and inAttackRange
    private bool autoAttacking;
    public bool inAttackRange;
    private bool attackStarted;
    // Declare variable for combat message
    private string combatMessage;
    // Start is called before the first frame update
    void Start()
    {
        // Set the enemyAttackRange, enemyAttackDelay and enemyAttackDamage
        enemyAttackRange = 4;
        enemyAttackDamage = 10;
        enemyAttackDelay = 2.0f;
        // Set the player  and chat window game objects
        player = GameObject.Find("Zidgog");
        chatUIWindow = GameObject.Find("Chat UI Window");
        // Set attackStarted
        attackStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the AutoAttack method
        AutoAttack();
        // Call the IsInAttackRange method
        IsInAttackRange();
        // Call the IsAutoAttacking method
        IsAutoAttacking();
    }
    // Create a method to determine if the player is in attack range
    private void IsInAttackRange()
    {
        // Set the distance between the enemy and the player
        float distance = Vector3.Distance(transform.position, player.transform.position);
        // If distance is less than or equal to attack range inAttackRange is true
        if (distance <= enemyAttackRange)
        {
            inAttackRange = true;
        } else
        {
            inAttackRange = false;
        }
    }
    // Create a method to determine if the enemy is auto attacking
    private void IsAutoAttacking()
    {
        // Set autoAttacking to true if the enemy is aggro
        if (GetComponent<EnemyController>().isAggro)
        {
            autoAttacking = true;
        } else
        {
            autoAttacking = false;
        }
    }
    // Create a method for the enemy to  auto attack
    private void AutoAttack()
    {
        // If the player is autoattacking start the attack coroutine
        if (autoAttacking & !attackStarted)
        {
            attackStarted = !attackStarted;
            StartCoroutine("Attack");
        }
    }
    // Create a coroutine for the enemy attack
    IEnumerator Attack()
    {
        while(autoAttacking)
        {
            if (inAttackRange)
            {
                // Call the player take damage method and set the combatMessage
                player.GetComponent<PlayerController>().TakeDamage(enemyAttackDamage);
                combatMessage = transform.name.Replace("(Clone)", "").Trim() + " has hit you for " +
                                enemyAttackDamage + " points of damage!";
                // Call the SetChatLogText method
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                if (player.GetComponent<PlayerController>().currentHealth <= 0)
                {
                    combatMessage = transform.name.Replace("(Clone)", "").Trim() + " has killed you!";
                    // Call the SetChatLogText method
                    chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                    StopCoroutine("Attack");
                    attackStarted = false;
                    autoAttacking = false;
                    GetComponent<EnemyController>().isAggro = false;
                }
            }
            yield return new WaitForSeconds(enemyAttackDelay);
        }
    }
}
