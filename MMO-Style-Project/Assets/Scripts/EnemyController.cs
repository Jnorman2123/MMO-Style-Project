using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Declare variable for the enemy character controller
    public CharacterController enemyController;
    // Declare variables for the enemy max health and enemy current health
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        // Set the currentHealth to the maxHealth
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        // Call the Death method
        Death();
    }
    // Create method to set the health of the enemy based on its type
    public void SetMaxHealth()
    {
        // Use a switch case statement to set the enemy health based on the enemy name
        string name = gameObject.name;
        switch(name)
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
    // Create method to take damage based on the player attack damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Current health " + currentHealth);
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
