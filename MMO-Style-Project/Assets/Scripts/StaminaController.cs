using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaController : MonoBehaviour
{
    // Declare variables for max and current stamina
    public int maxStamina;
    public int currentStamina;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetMaxStamina method
        SetMaxStamina();
        // Set currentStamina to maxStamina
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Declare a method to determine max stamina
    private void SetMaxStamina()
    {
        if (CompareTag("Player"))
        {
            maxStamina = 75;
        } else if (CompareTag("Enemy"))
        {
            // Use a switch case statement to set the enemy stamina based on the enemy name
            string name = gameObject.name;
            switch (name)
            {
                case "Warrior(Clone)":
                    maxStamina = 75;
                    break;
                case "Rogue(Clone)":
                    maxStamina = 60;
                    break;
                case "Wizard(Clone)":
                    maxStamina = 50;
                    break;
                case "Cleric(Clone)":
                    maxStamina = 60;
                    break;
                case "Captain(Clone)":
                    maxStamina = 100;
                    break;
                case "Key Master(Clone)":
                    maxStamina = 125;
                    break;
            }
        }
    }
}
