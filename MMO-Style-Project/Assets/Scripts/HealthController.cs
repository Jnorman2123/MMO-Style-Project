using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    // Declare variables for player current and max health and mana
    public int currentHealth;
    public int maxHealth;
    // Declare variables for healthRegen and regenDelay and isRegeningHealth
    private int healthRegen;
    private int regenDelay = 5;
    private bool isRegeningHealth;
    // Declare variable for chat ui window and combatMessage
    private string combatMessage;
    private GameObject chatUIWindow;
    // Start is called before the first frame update
    void Start()
    {
        // Set the ChatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
        // Set the maxHealth
        SetMaxHealth();
        // Set the currentHealth and regening false
        currentHealth = maxHealth;
        isRegeningHealth = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the RegenHealth coroutine if the current health is less than the max health and not already regening
        if (currentHealth < maxHealth & !isRegeningHealth)
        {
            StartCoroutine("RegenHealth");
        }
    }
    // Create method to set the health of the character based on its stamina
    public void SetMaxHealth()
    {
        // Set the maxHealth based on the currentStamina
        int stamina = GetComponent<StatsController>().stamina;
        maxHealth = Mathf.RoundToInt(stamina * 1.5f);
    }
    // Create a method to damage the player to test the health bar
    public void TakeDamage(int damage, GameObject attacker, GameObject target)
    {
        currentHealth -= damage;
        // Make sure current health does not exceed max health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        // If the game object is an enemy call the gain hate method
        if (CompareTag("Enemy") & damage > 0)
        {
            GetComponent<EnemyController>().GainHate(damage, attacker);
        }
    }
    // Create a coroutine to regen the health of the player over time
    IEnumerator RegenHealth()
    {
        // While less than max health start regening health
        while (currentHealth < maxHealth)
        {
            if (CompareTag("Player"))
            {
                // Set the healthRegen based on in combat or not
                if (GetComponent<CombatController>().inCombat)
                {
                    healthRegen = 1;
                }
                else
                {
                    healthRegen = 2;
                }
            } else if (CompareTag("Enemy"))
            {
                // Set the healthRegen based on in combat or not
                if (GetComponent<EnemyCombatController>().inCombat)
                {
                    healthRegen = 1;
                }
                else
                {
                    healthRegen = 2;
                }
            }
            isRegeningHealth = true;
            currentHealth += healthRegen;
            // If health is greater than max set it to the max
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            // If health is equal to the max then stop regening health
            if (currentHealth == maxHealth)
            {
                StopCoroutine("RegenHealth");
                isRegeningHealth = false;
            }
            yield return new WaitForSeconds(regenDelay);
        }
    }
}
