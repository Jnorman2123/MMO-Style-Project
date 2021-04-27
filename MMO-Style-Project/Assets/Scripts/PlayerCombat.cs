using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Declare variables for attack delay, attack damage, and attack range
    private float playerAttackDelay = 2.0f;
    private int playerAttackDamage = 50;
    private float playerAttackRange = 10.0f;
    // Declare variable for the player target
    private GameObject playerTarget;
    // Declare variable for chat window ui
    public GameObject chatUIWindow;
    // Declare variables for auto attacking and inAttackRange and inCombat
    private bool autoAttacking;
    public bool inAttackRange;
    public bool inCombat;
    // Declare variable for combat message
    private string combatMessage;
    // Start is called before the first frame update
    void Start()
    {
        // Set autoAttacking and inCombat to false at start
        autoAttacking = false;
        inCombat = false;
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
        playerTarget = GetComponent<TargetingController>().target;
        // Call the enemy TakeDamage method with the damage 
        playerTarget.GetComponent<HealthController>().TakeDamage(playerAttackDamage);
        // Set the combat message to reflect how much damage you deal to the enemy
        combatMessage = "You hit " + playerTarget.name.Replace("(Clone)", "").Trim() + " for " + playerAttackDamage + " points of damage!";
        // Call the SetChatLogText method
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
    }
    // Create method to toggle auto attack
    private void AutoAttack()
    {
        // If player is targeting an enemy and presses tilde toggle auto attack
        if (Input.GetKeyDown("`"))
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
            } else
            {
                // Set combat message to show you stop attacking
                combatMessage = "You are no longer auto attacking.";
                // Call the SetChatLogText method
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
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
        playerTarget = GetComponent<TargetingController>().target;
        if (playerTarget != null)
        {
            RaycastHit hit;
            // Use a raycast to determine if the player is in attack range of the target
            if (Physics.Raycast(transform.position, transform.forward, out hit, playerAttackRange, layerMask))
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
        playerTarget = GetComponent<TargetingController>().target;
        // Repeat while autoAttacking is true
        while(autoAttacking)
        {
            // If the playerTarget is equal to null log "You have no target."
            if (playerTarget == null)
            {
                combatMessage = "You have no target.";
                // Call the SetChatLogText method
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            } else {
                // Use a switch case statement to take certain action based on the playerTarget tag
                string tag = playerTarget.gameObject.tag;
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
                            if (playerTarget.GetComponent<HealthController>().currentHealth <= 0)
                            {
                                combatMessage = "You have defeated " + playerTarget.name.Replace("(Clone)", "").Trim() + "!" + 
                                                "\r\nYou have gained experience!" +
                                                "\r\nYou are no longer auto attacking."  ;
                                // Call the GainExp method from the player controller
                                GetComponent<PlayerController>().GainExp();
                                // Call the SetChatLogText method
                                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                                StopCoroutine("Attack");
                                playerTarget = null;
                                autoAttacking = false;
                                inCombat = false;
                            }
                        }
                        break;
                }
            }
            yield return new WaitForSeconds(playerAttackDelay);
        }
    }
}
