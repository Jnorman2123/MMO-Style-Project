﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare variables for player current and max health and mana
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    // Declare variables for player experience and player lvl
    public int currentExp;
    public int maxExp;
    public int playerLevel;
    // Declare variables for the player ui window, exp ui window, and chat ui window
    public GameObject playerUIWindow;
    public GameObject expUIWindow;
    public GameObject chatUIwindow;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current health
        maxHealth = 200;
        currentHealth = maxHealth;
        // Set the max and current mana
        maxMana = 100;
        currentMana = maxMana;
        // Set the max and current exp and player level
        maxExp = 100;
        currentExp = 0;
        playerLevel = 1;
        // Call the SetMaxHealthBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetPlayerMaxHealthBarValue();
        // Call the SetMaxManaBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetPlayerMaxManaBarValue();
        // Call the SetExpBarMaxValue method
        expUIWindow.GetComponent<ExpWindowController>().SetExpBarMaxValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the UseMana method if the player presses the g key
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseMana();
        }
        // Call the LevelUp method
        LevelUp();
    }
    // Create a method to damage the player to test the health bar
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    // Create method to simulate using mana to test mana bar
    private void UseMana()
    {
        currentMana -= 10;
    }
    // Create a method to gain experience
    public void GainExp()
    {
        // Gain exp
        currentExp += 55;
    }
    // Create a method to gain level when max exp is reached
    private void LevelUp()
    {
        // When max exp is reached gain a level, set a new maxExp and reset currentExp to the roll over exp
        if (currentExp >= maxExp)
        {   
            int rollOverExp = currentExp - maxExp;
            playerLevel++;
            maxExp *= 2;
            currentExp = rollOverExp;
            string levelUpMessage = "Congratulations you are now level " + playerLevel + "!";
            // Display a message to show the player gained a level
            chatUIwindow.GetComponent<ChatWindowController>().SetChatLogText(levelUpMessage);
        }
    }
}
