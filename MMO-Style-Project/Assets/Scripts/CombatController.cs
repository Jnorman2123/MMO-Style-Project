using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    // Declare variables for attack delay, attack damage, and attack range
    public float attackDelay;
    public int attackDamage;
    private float attackRange;
    // Declare variable for the target
    private GameObject target;
    // Declare variable for chat window ui
    private GameObject chatUIWindow;
    // Declare variables for auto attacking and inAttackRange and inCombat and isTauntingStrike
    public bool autoAttacking;
    public bool inAttackRange;
    public bool inCombat;
    public bool isTauntingStrike;
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
        // If player presses tilde the player will start or stop autoattacking
        if (Input.GetKeyDown("`"))
        {
            autoAttacking = !autoAttacking;
            AutoAttack();
        }
        // Call the InAttackRange method
        InAttackRange();
    }
    // Create method to set the attackDamage, attackDelay, and AttackRange
    private void SetAttackValues()
    {
        // Get strength and agility
        int strength = GetComponent<StatsController>().strength;
        int agility = GetComponent<StatsController>().agility;
        attackDamage = Mathf.RoundToInt(strength * 0.25f);
        attackDelay = 3.0f - (agility * 0.01f);
        attackRange = 5.0f;
    }
    // Create method to attack the target if it is an enemy
    private void DamageTarget(GameObject target)
    {
        // Randomize damage
        int minDamage = attackDamage / 2;
        int maxDamage = attackDamage;
        int randomDamage = Random.Range(minDamage, maxDamage);
        // Account for armor
        int armor = target.GetComponent<ArmorController>().armor;
        int netDamage = randomDamage - Mathf.RoundToInt(armor / 4);
        if (netDamage <= 0)
        {
            netDamage = 1;
        }
        // Call the TakeDamage method with the damage 
        target.GetComponent<HealthController>().TakeDamage(netDamage, gameObject, target.gameObject);
        // If the attack is a taunting strike
        // Set the combat message based on the attacker, target, and damage
        combatMessage = gameObject.name + " hit " + target.name.Replace("(Clone)", "").Trim()
            + " for " + netDamage + " points of damage!";
        // Call the SetChatLogText method
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
    }
    // Create method to toggle auto attack
    public void AutoAttack()
    {
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
    }
    // Create method to determine if the player is in attack range
    private void InAttackRange()
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
    // Create IEnum to attack with attackDelay
    IEnumerator Attack()
    {
        // Repeat while autoAttacking is true
        while (autoAttacking)
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
                            DamageTarget(target);
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
            yield return new WaitForSeconds(attackDelay);
        }
    }
}
