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
            stamina = 75;
            strength = 80;
            intelligence = 40;
            wisdom = 40;
            agility = 60;
            dexterity = 60;
            charisma = 50;
            willpower = 50;
        }
        else if (CompareTag("Enemy"))
        {
            // Use a switch case statement to set the enemy stamina based on the enemy name
            string name = gameObject.name.Replace("(Clone)", "").Trim();
            switch (name)
            {
                case "Warrior":
                    stamina = 75;
                    strength = 60;
                    intelligence = 40;
                    wisdom = 40;
                    agility = 60;
                    dexterity = 60;
                    charisma = 50;
                    willpower = 50;
                    break;
                case "Rogue":
                    stamina = 60;
                    strength = 60;
                    intelligence = 40;
                    wisdom = 40;
                    agility = 70;
                    dexterity = 70;
                    charisma = 60;
                    willpower = 50;
                    break;
                case "Wizard":
                    stamina = 50;
                    strength = 40;
                    intelligence = 80;
                    wisdom = 50;
                    agility = 50;
                    dexterity = 40;
                    charisma = 50;
                    willpower = 60;
                    break;
                case "Cleric":
                    stamina = 60;
                    strength = 50;
                    intelligence = 50;
                    wisdom = 80;
                    agility = 50;
                    dexterity = 40;
                    charisma = 50;
                    willpower = 60;
                    break;
                case "Captain":
                    stamina = 100;
                    strength = 75;
                    intelligence = 40;
                    wisdom = 40;
                    agility = 60;
                    dexterity = 60;
                    charisma = 50;
                    willpower = 50;
                    break;
                case "Key Master":
                    stamina = 125;
                    strength = 80;
                    intelligence = 40;
                    wisdom = 40;
                    agility = 60;
                    dexterity = 60;
                    charisma = 50;
                    willpower = 50;
                    break;
            }
        }
    }
}
