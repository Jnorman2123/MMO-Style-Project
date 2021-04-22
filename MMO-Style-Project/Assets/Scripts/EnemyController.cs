using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Declare variables for the enemy max and current health and mana
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    // Declare variable for enemy is patrol
    public bool isPatrol;
    // Start is called before the first frame update
    void Start()
    {
        // Set the currentHealth to the maxHealth and currentMana to the maxMana
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    // Update is called once per frame
    void Update()
    {
        // Call the Death method
        Death();
        // Call the UseMana method
        UseMana();
    }
    // Create method to set the health and mana of the enemy based on its type
    public void SetMaxHealthMana()
    {
        // Use a switch case statement to set the enemy health and mana based on the enemy name
        string name = gameObject.name;
        switch(name)
        {
            case "Warrior(Clone)":
                maxHealth = 200;
                maxMana = 50;
                break;
            case "Rogue(Clone)":
                maxHealth = 175;
                maxMana = 50;
                break;
            case "Wizard(Clone)":
                maxHealth = 125;
                maxMana = 150;
                break;
            case "Cleric(Clone)":
                maxHealth = 150;
                maxMana = 100;
                break;
            case "Captain(Clone)":
                maxHealth = 250;
                maxMana = 150;
                break;
            case "Key Master(Clone)":
                maxHealth = 300;
                maxMana = 200;
                break;
        }
    }
    // Create method to take damage based on the player attack damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    // Create a method to test out targets using mana
    private void UseMana()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentMana -= 10;
        }
    }
    // Create a method to destroy to object when health reaches zero
    private void Death()
    {
        // Check to see if health is equal or less than zero and destroy to the object
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
