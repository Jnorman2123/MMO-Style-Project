using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    // Declare variables for max and current stamina
    public int maxStamina;
    public int currentStamina;
    // Declare variables for max ans current strength
    public int maxStrength;
    public int currentStrength;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetMaxStats method
        SetMaxStats();
        // Call the SetCurrentStats method
        SetCurrentStats();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Declare a method to determine max stamina
    private void SetMaxStats()
    {
        if (CompareTag("Player"))
        {
            // Set player stats
            maxStamina = 75;
            maxStrength = 80;
        }
        else if (CompareTag("Enemy"))
        {
            // Use a switch case statement to set the enemy stamina based on the enemy name
            string name = gameObject.name;
            switch (name)
            {
                case "Warrior(Clone)":
                    maxStamina = 75;
                    maxStrength = 60;
                    break;
                case "Rogue(Clone)":
                    maxStamina = 60;
                    maxStrength = 60;
                    break;
                case "Wizard(Clone)":
                    maxStamina = 50;
                    maxStrength = 40;
                    break;
                case "Cleric(Clone)":
                    maxStamina = 60;
                    maxStrength = 50;
                    break;
                case "Captain(Clone)":
                    maxStamina = 100;
                    maxStrength = 75;
                    break;
                case "Key Master(Clone)":
                    maxStamina = 125;
                    maxStrength = 80;
                    break;
            }
        }
    }
    // Create a method to set the current stat to the max stat
    private void SetCurrentStats()
    {
        currentStamina = maxStamina;
        currentStrength = maxStrength;
    }
}
