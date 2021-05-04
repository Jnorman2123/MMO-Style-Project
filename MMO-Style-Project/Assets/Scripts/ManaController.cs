using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    // Declare variables for player current and max mana
    public int currentMana;
    public int maxMana;
    // Declare variables for manaRegen, regenDelay, and isRegeningMana
    private int manaRegen;
    private float regenDelay = 5.0f;
    private bool isRegeningMana;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current mana
        SetMaxMana();
        currentMana = maxMana;
        // Set isRegeningMana to false
        isRegeningMana = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Start the RegenMana coroutine if the current mana is less than the max mana and not already regening
        if (currentMana < maxMana & !isRegeningMana)
        {
            StartCoroutine(RegenMana());
        }
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
    // Create a method to use mana
    public void UseMana(int manaCost)
    {
        currentMana -= manaCost;
    }
    // Create a coroutine to regen the mana of the player over time
    IEnumerator RegenMana()
    {
        // While less than max mana start regening mana
        while (currentMana < maxMana)
        {
            if (CompareTag("Player"))
            {
                // Set the healthRegen based on in combat or not
                if (GetComponent<CombatController>().inCombat)
                {
                   manaRegen = 1;
                }
                else
                {
                    manaRegen = 2;
                }
            }
            else if (CompareTag("Enemy"))
            {
                // Set the healthRegen based on in combat or not
                if (GetComponent<EnemyCombatController>().inCombat)
                {
                    manaRegen = 1;
                }
                else
                {
                    manaRegen = 2;
                }
            }
            isRegeningMana = true;
            currentMana += manaRegen;
            // If health is greater than max set it to the max
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            // If health is equal to the max then stop regening health
            if (currentMana == maxMana)
            {
                StopCoroutine("RegenHealth");
                isRegeningMana = false;
            }
            yield return new WaitForSeconds(regenDelay);
        }
    }
}
