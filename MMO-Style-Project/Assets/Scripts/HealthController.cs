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
    private int regenDelay = 2;
    private bool isRegeningHealth;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current health and regen delay and is regening health
        if (transform.CompareTag("Player"))
        {
            maxHealth = 100;
        } else if (transform.CompareTag("Enemy")) {
            SetMaxHealth();
        }
        
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
        if (CompareTag("Player") & currentHealth <= 0)
        {
            GetComponent<PlayerController>().PlayerDeath();
            currentHealth = maxHealth / 2;
        }
    }
    // Create method to set the health and mana of the enemy based on its type
    public void SetMaxHealth()
    {
        // Use a switch case statement to set the enemy health and mana based on the enemy name
        string name = gameObject.name;
        switch (name)
        {
            case "Warrior(Clone)":
                maxHealth = 200;
                break;
            case "Rogue(Clone)":
                maxHealth = 175;
                break;
            case "Wizard(Clone)":
                maxHealth = 125;
                break;
            case "Cleric(Clone)":
                maxHealth = 150;
                break;
            case "Captain(Clone)":
                maxHealth = 250;
                break;
            case "Key Master(Clone)":
                maxHealth = 300;
                break;
        }
    }
    // Create a method to damage the player to test the health bar
    public void TakeDamage(int damage, GameObject attacker)
    {
        currentHealth -= damage;
        // If the game object is an enemy call the gain hate method
        if (transform.CompareTag("Enemy"))
        {
            transform.GetComponent<EnemyController>().GainHate(damage, attacker);
        }
    }
    // Create a coroutine to regen the health of the player over time
    IEnumerator RegenHealth()
    {
        // While less than max health start regening health
        while (currentHealth < maxHealth)
        {
            // Set the healthRegen based on in combat or not
            if (GetComponent<PlayerCombat>().inCombat)
            {
                healthRegen = 1;
            }
            else
            {
                healthRegen = 2;
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
