using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare variables for player current and max health
    public int currentHealth;
    public int maxHealth;
    // Declare variable for the player ui window 
    public GameObject playerUIWindow;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current health
        maxHealth = 200;
        currentHealth = maxHealth;
        // Call the SetMaxHealthBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetMaxHealthBarValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the TakeDamage method if the player presses the f key
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage();
        }
    }
    // Create a method to damage the player to test the health bar
    private void TakeDamage()
    {
        currentHealth -= 50;
    }
}
