using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    // Declare variable for stamina
    public int stamina;
    // Declare variable for strength
    public int strength;
    // Declare variablefor intelligence
    public int intelligence;
    // Declare variable for wisdom
    public int wisdom;
    // Declare variable for agility
    public int agility;
    // Declare variable for dexterity
    public int dexterity;
    // Declare variable for charisma
    public int charisma;
    // Declare variable for willpower
    public int willpower;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetStats method
        SetStats();
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Declare a method to determine max stamina
    private void SetStats()
    {
        if (CompareTag("Player"))
        {
            // Set player stats
            stamina = 25;
            strength = 30;
            intelligence = 15;
            wisdom = 15;
            agility = 20;
            dexterity = 20;
            charisma = 15;
            willpower = 20;
        }
        else if (CompareTag("Enemy"))
        {
            // Use a switch case statement to set the enemy stamina based on the enemy name
            string name = gameObject.name.Replace("(Clone)", "").Trim();
            switch (name)
            {
                case "Warrior":
                    stamina = 25;
                    strength = 30;
                    intelligence = 15;
                    wisdom = 15;
                    agility = 20;
                    dexterity = 20;
                    charisma = 15;
                    willpower = 20;
                    break;
                case "Rogue":
                    stamina = 20;
                    strength = 20;
                    intelligence = 15;
                    wisdom = 15;
                    agility = 30;
                    dexterity = 30;
                    charisma = 25;
                    willpower = 20;
                    break;
                case "Wizard":
                    stamina = 15;
                    strength = 15;
                    intelligence = 30;
                    wisdom = 20;
                    agility = 15;
                    dexterity = 15;
                    charisma = 20;
                    willpower = 25;
                    break;
                case "Cleric":
                    stamina = 20;
                    strength = 15;
                    intelligence = 20;
                    wisdom = 30;
                    agility = 15;
                    dexterity = 15;
                    charisma = 20;
                    willpower = 25;
                    break;
                case "Captain":
                    stamina = 40;
                    strength = 30;
                    intelligence = 15;
                    wisdom = 15;
                    agility = 25;
                    dexterity = 25;
                    charisma = 20;
                    willpower = 20;
                    break;
                case "Key Master":
                    stamina = 50;
                    strength = 35;
                    intelligence = 15;
                    wisdom = 15;
                    agility = 25;
                    dexterity = 25;
                    charisma = 20;
                    willpower = 20;
                    break;
            }
        }
    }
}
