using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    // Declare variables for player current and max mana
    public int currentMana;
    public int maxMana;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current mana
        SetMaxMana();
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Create method to set the mana based on stat
    private void SetMaxMana()
    {
        // Set variables for each stat
        int strength = GetComponent<StatsController>().strength;
        int intelligence = GetComponent<StatsController>().intelligence;
        int wisdom = GetComponent<StatsController>().wisdom;
        int agility = GetComponent<StatsController>().agility;
        // Set mana according to different stat based on character type
        if (CompareTag("Player"))
        {
            maxMana = Mathf.RoundToInt(strength * 1.25f);
        } else if (CompareTag("Enemy"))
        {
            // Use switch case statement to determine which stat to base mana off
            string name = gameObject.name.Replace("(Clone)", "").Trim();
            switch(name)
            {
                case "Warrior":
                    maxMana = Mathf.RoundToInt(strength * 1.25f);
                    break;
                case "Wizard":
                    maxMana = Mathf.RoundToInt(intelligence * 1.25f);
                    break;
                case "Rogue":
                    maxMana = Mathf.RoundToInt(agility * 1.25f);
                    break;
                case "Cleric":
                    maxMana = Mathf.RoundToInt(wisdom * 1.25f);
                    break;
                case "Key Master":
                    maxMana = Mathf.RoundToInt(strength * 1.25f);
                    break;
                case "Captain":
                    maxMana = Mathf.RoundToInt(strength * 1.25f);
                    break;
            }
        }
    }
}
