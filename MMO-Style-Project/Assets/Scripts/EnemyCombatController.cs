using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatController : MonoBehaviour
{
    // Declare variables for attack delay, attack damage, and attack range
    public float attackDelay;
    public int attackDamage;
    private float attackRange;
    // Declare variable for the target
    private GameObject target;
    // Declare variable for chat window ui and combatMessage
    private GameObject chatUIWindow;
    private string combatMessage;
    // Declare variables for auto attacking and inAttackRange and inCombat
    public bool autoAttacking;
    public bool inAttackRange;
    public bool inCombat;
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
        // Call the InAttackRange method
        InAttackRange();
        // Call the IsAutoAttacking method
        IsAutoAttacking();  
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
    // Create method to determine if the player is in attack range
    private void InAttackRange()
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
    // Create a method to determine if the enemy is auto attacking
    private void IsAutoAttacking()
    { // Set autoAttacking to true if the enemy is aggro
        if (GetComponent<EnemyController>().isAggro)
        {
            autoAttacking = true;
            AutoAttack();
        }
        else
        {
            autoAttacking = false;
        }
    }
    // Create method to toggle auto attack
    public void AutoAttack()
    {
        // If the enemy is autoattacking start the attack coroutine
        if (autoAttacking & !inCombat)
        {
            inCombat = !inCombat;
            StartCoroutine(Attack());
        }
    }
    // Create IEnum to attack with attackDelay
    IEnumerator Attack()
    {
        
        while (autoAttacking)
        {
            if (inAttackRange)
            {
                Debug.Log("attack");
                // Call the player take damage method
                DamageTarget(target);
                if (target.GetComponent<HealthController>().currentHealth <= 0)
                {
                    combatMessage = transform.name.Replace("(Clone)", "").Trim() + " has killed you!";
                    // Call the SetChatLogText method
                    chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
                    StopCoroutine(Attack());
                    inCombat = false;
                    autoAttacking = false;
                    GetComponent<EnemyController>().isAggro = false;
                }
            }
            yield return new WaitForSeconds(attackDelay);
        }
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
        combatMessage = gameObject.name.Replace("(Clone)", "").Trim() + " hit " + target.name
            + " for " + netDamage + " points of damage!";
        // Call the SetChatLogText method
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
    }
}
