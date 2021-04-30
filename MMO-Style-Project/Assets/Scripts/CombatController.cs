using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    // Declare variables for attack delay, attack damage, and attack range
    private float attackDelay;
    private int attackDamage;
    private float attackRange;
    // Declare variable for the target
    private GameObject target;
    // Declare variable for chat window ui
    private GameObject chatUIWindow;
    // Declare variables for auto attacking and inAttackRange and inCombat
    private bool autoAttacking;
    public bool inAttackRange;
    public bool inCombat;
    // Declare variable for combat message
    private string combatMessage;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetAttackValues method
        SetAttackValues();
        // Set autoAttacking and inCombat to false at start
        autoAttacking = false;
        inCombat = false;
        // Set the chat window game object
        chatUIWindow = GameObject.Find("Chat UI Window");
    }

    // Update is called once per frame
    void Update()
    {
        // Call the IsAutoAttacking method if the gameObject is an enemy
        if (CompareTag("Enemy"))
        {
            IsAutoAttacking();
        }
        // Call the AutoAttack method
        AutoAttack();
        // Call the InAttackRange method
        InAttackRange();
    }
    // Create method to set the attackDamage, attackDelay, and AttackRange
    private void SetAttackValues()
    {
        if (CompareTag("Player"))
        {
            attackDelay = 2.0f;
            attackDamage = 50;
            attackRange = 5.0f;

        } else if (CompareTag("Enemy"))
        {
            attackDelay = 2.0f;
            attackDamage = 25;
            attackRange = 5.0f;
        }
    }
    // Create method to attack the target if it is an enemy
    private void DamageTarget()
    {
        // Set the target to the game object being targeted
        target = GetComponent<TargetingController>().target;
        // Call the TakeDamage method with the damage 
        target.GetComponent<HealthController>().TakeDamage(attackDamage, transform.gameObject);
        // Set the combat message to reflect how much damage you deal to the enemy
        combatMessage = "You hit " + target.name.Replace("(Clone)", "").Trim() + " for "
                        + attackDamage + " points of damage!";
        // Call the SetChatLogText method
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
    }
    // Create method to toggle auto attack
    private void AutoAttack()
    {
        // If player is targeting an enemy and presses tilde toggle auto attack
        if (CompareTag("Player") & Input.GetKeyDown("`"))
        {
            autoAttacking = !autoAttacking;
            // Check to see if autoAttacking and start or stop the attack coroutine
            if (autoAttacking)
            {
                // Set combat message to show you began attacking
                combatMessage = "You have started auto attacking.";
                // Call the SetChatLogText method
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                // Start the Attack coroutine
                StartCoroutine("Attack");
            }
            else
            {
                // Set combat message to show you stop attacking
                combatMessage = "You are no longer auto attacking.";
                // Call the SetChatLogText method
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                // Stop the Attack coroutine
                StopCoroutine("Attack");
            }
        } else if (CompareTag("Enemy"))
        {
            // If the enemy is autoattacking start the attack coroutine
            if (autoAttacking & !inCombat)
            {
                inCombat = !inCombat;
                StartCoroutine("Attack");
            }
        }
    }
    // Create method to determine if the player is in attack range
    private void InAttackRange()
    {
        if (CompareTag("Player"))
        {
            // Set the layer mask to the targetable layer only
            int layerMask = (1 << 8);
            // Set the target to the game object being targeted
            target = GetComponent<TargetingController>().target;
            if (target != null)
            {
                RaycastHit hit;
                // Use a raycast to determine if the player is in attack range of the target
                if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, layerMask))
                {
                    // Create variable for the game object of the hit parent
                    GameObject hitObject = hit.transform.parent.gameObject;
                    // Check if the hitObject is an enemy and the current target of the player then set inAttackRange
                    if (hitObject.CompareTag("Enemy") & hitObject.name == target.name)
                    {
                        inAttackRange = true;
                    }
                }
                else
                {
                    inAttackRange = false;
                }
            }
            else
            {
                inAttackRange = false;
            }
        }
        if (CompareTag("Enemy"))
        {
            // Set the target to the enemy target
            target = GetComponent<EnemyController>().enemyTarget;
            if (target != null)
            {
                // Set the distance between the enemy and the player
                float distance = Vector3.Distance(transform.position, target.transform.position);
                // If distance is less than or equal to attack range inAttackRange is true
                if (distance <= attackRange)
                {
                    inAttackRange = true;
                }
                else
                {
                    inAttackRange = false;
                }
            }
        }
    }
    // Create a method to determine if the enemy is auto attacking
    private void IsAutoAttacking()
    {
        // Set autoAttacking to true if the enemy is aggro
        if (GetComponent<EnemyController>().isAggro)
        {
            autoAttacking = true;
        }
        else
        {
            autoAttacking = false;
        }
    }
    // Create IEnum to attack with attackDelay
    IEnumerator Attack()
    {
        // Repeat while autoAttacking is true
        while (autoAttacking)
        {
            if (CompareTag("Player"))
            {
                // Set the playerTarget to the game object being targeted
                target = GetComponent<TargetingController>().target;
                // If the playerTarget is equal to null log "You have no target."
                if (target == null)
                {
                    combatMessage = "You have no target.";
                    // Call the SetChatLogText method
                    chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                }
                else
                {
                    // Use a switch case statement to take certain action based on the playerTarget tag
                    string tag = target.gameObject.tag;
                    switch (tag)
                    {
                        case "Player":
                            combatMessage = "You can't attack yourself";
                            // Call the SetChatLogText method
                            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                            break;
                        case "Interactable":
                            combatMessage = "You cannot attack your current target.";
                            // Call the SetChatLogText method
                            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                            break;
                        case "Enemy":
                            if (!inAttackRange)
                            {
                                combatMessage = "You are too far away from your target.";
                                // Call the SetChatLogText method
                                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                            }
                            else
                            {
                                // Set inCombat to true and call the DamageTarget method
                                inCombat = true;
                                DamageTarget();
                                if (target.GetComponent<HealthController>().currentHealth <= 0)
                                {
                                    combatMessage = "You have defeated " + target.name.Replace("(Clone)", "").Trim() + "!" +
                                                    "\r\nYou have gained experience!" +
                                                    "\r\nYou are no longer auto attacking.";
                                    // Call the GainExp method from the player controller
                                    GetComponent<PlayerController>().GainExp();
                                    // Call the SetChatLogText method
                                    chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                                    StopCoroutine("Attack");
                                    target = null;
                                    autoAttacking = false;
                                    inCombat = false;
                                }
                            }
                            break;
                    }
                }
            } else if (CompareTag("Enemy"))
            {
                // Set the enemy target
                target = GetComponent<EnemyController>().enemyTarget;
                if (inAttackRange)
                {
                    // Call the player take damage method and set the combatMessage
                    target.GetComponent<HealthController>().TakeDamage(attackDamage, transform.gameObject);
                    combatMessage = transform.name.Replace("(Clone)", "").Trim() + " has hit you for " +
                                    attackDamage + " points of damage!";
                    // Call the SetChatLogText method
                    chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                    if (target.GetComponent<HealthController>().currentHealth <= 0)
                    {
                        combatMessage = transform.name.Replace("(Clone)", "").Trim() + " has killed you!";
                        // Call the SetChatLogText method
                        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                        StopCoroutine("Attack");
                        inCombat = false;
                        autoAttacking = false;
                        GetComponent<EnemyController>().isAggro = false;
                    }
                }
            }
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
