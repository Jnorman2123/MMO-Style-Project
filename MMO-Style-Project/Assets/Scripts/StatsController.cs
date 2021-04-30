using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    // Declare variables for max and current stamina
    public int maxStamina;
    public int currentStamina;
    // Declare variables for max and current strength
    public int maxStrength;
    public int currentStrength;
    // Declare variables for max and current intelligence
    public int maxIntelligence;
    public int currentIntelligence;
    // Declare variables for max and current wisdom
    public int maxWisdom;
    public int currentWisdom;
    // Declare variables for max and current agility
    public int maxAgility;
    public int currentAgility;
    // Declare variables for max and current dexterity
    public int maxDexterity;
    public int currentDexterity;
    // Declare variables for max and current charisma
    public int maxCharisma;
    public int currentCharisma;
    // Declare variables for max and current willpower
    public int maxWillpower;
    public int currentWillpower;
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
            maxIntelligence = 40;
            maxWisdom = 40;
            maxAgility = 60;
            maxDexterity = 60;
            maxCharisma = 50;
            maxWillpower = 50;
        }
        else if (CompareTag("Enemy"))
        {
            // Use a switch case statement to set the enemy stamina based on the enemy name
            string name = gameObject.name.Replace("(Clone)", "").Trim();
            switch (name)
            {
                case "Warrior":
                    maxStamina = 75;
                    maxStrength = 60;
                    maxIntelligence = 40;
                    maxWisdom = 40;
                    maxAgility = 60;
                    maxDexterity = 60;
                    maxCharisma = 50;
                    maxWillpower = 50;
                    break;
                case "Rogue":
                    maxStamina = 60;
                    maxStrength = 60;
                    maxIntelligence = 40;
                    maxWisdom = 40;
                    maxAgility = 70;
                    maxDexterity = 70;
                    maxCharisma = 60;
                    maxWillpower = 50;
                    break;
                case "Wizard":
                    maxStamina = 50;
                    maxStrength = 40;
                    maxIntelligence = 80;
                    maxWisdom = 50;
                    maxAgility = 50;
                    maxDexterity = 40;
                    maxCharisma = 50;
                    maxWillpower = 60;
                    break;
                case "Cleric":
                    maxStamina = 60;
                    maxStrength = 50;
                    maxIntelligence = 50;
                    maxWisdom = 80;
                    maxAgility = 50;
                    maxDexterity = 40;
                    maxCharisma = 50;
                    maxWillpower = 60;
                    break;
                case "Captain":
                    maxStamina = 100;
                    maxStrength = 75;
                    maxIntelligence = 40;
                    maxWisdom = 40;
                    maxAgility = 60;
                    maxDexterity = 60;
                    maxCharisma = 50;
                    maxWillpower = 50;
                    break;
                case "Key Master":
                    maxStamina = 125;
                    maxStrength = 80;
                    maxIntelligence = 40;
                    maxWisdom = 40;
                    maxAgility = 60;
                    maxDexterity = 60;
                    maxCharisma = 50;
                    maxWillpower = 50;
                    break;
            }
        }
    }
    // Create a method to set the current stat to the max stat
    private void SetCurrentStats()
    {
        currentStamina = maxStamina;
        currentStrength = maxStrength;
        currentAgility = maxAgility;
        currentIntelligence = maxIntelligence;
        currentWisdom = maxWisdom;
        currentDexterity = maxDexterity;
        currentCharisma = maxCharisma;
        currentWillpower = maxWillpower;
    }
}
