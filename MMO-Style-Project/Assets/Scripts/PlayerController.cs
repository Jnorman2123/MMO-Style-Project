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
    // Declare variable for the player ui window 
    public GameObject playerUIWindow;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current health
        maxHealth = 200;
        currentHealth = maxHealth;
        // Set the max and current mana
        maxMana = 100;
        currentMana = maxMana;
        // Call the SetMaxHealthBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetPlayerMaxHealthBarValue();
        // Call the SetMaxManaBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetPlayerMaxManaBarValue();
        // Set the max and current exp and player level
        maxExp = 100;
        currentExp = 0;
        playerLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the TakeDamage method if the player presses the f key
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage();
        }
        // Call the UseMana method if the player presses the g key
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseMana();
        }
        // Call the LevelUp method
        LevelUp();
    }
    // Create a method to damage the player to test the health bar
    private void TakeDamage()
    {
        currentHealth -= 50;
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
        currentExp += 10;
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
        }
    }
}
