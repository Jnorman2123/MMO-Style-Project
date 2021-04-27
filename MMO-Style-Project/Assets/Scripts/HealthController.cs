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
    private int regenDelay;
    private bool isRegeningHealth;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current health and regen delay and is regening health
        maxHealth = 100;
        currentHealth = maxHealth;
        regenDelay = 2;
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
    // Create a method to damage the player to test the health bar
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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
