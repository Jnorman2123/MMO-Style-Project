using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerWindowController : MonoBehaviour
{
    // Declare variable for the player window ui text
    public TextMeshProUGUI playerNameText;
    // Declare variable for player name
    private string playerName = "Zidgog";
    // Declare variable for the player game object
    public GameObject playerObj;
    // Declare variables for the player health and mana bar sliders
    public Slider playerHealthBar;
    public Slider playerManaBar;
    // Declare variables for current and max health and mana
    private int maxHealth;
    private int currentHealth;
    private int maxMana;
    private int currentMana;
    // Start is called before the first frame update
    void Start()
    {
        // Set the text of the player name window
        playerNameText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SetHealthBarValue method
        SetHealthBarValue();
        // Call the SetManaBarValue method
        SetManaBarValue();
    }
    // Create a method to set the health bar health to the players current health
    private void SetHealthBarValue()
    {
        // Set currentHealth to the players current health
        currentHealth = playerObj.GetComponent<PlayerController>().currentHealth;
        // Set the playerHealthBar value to currentHealth
        playerHealthBar.value = currentHealth;
    }
    // Create a method to set the max value of the playerHealthBar to the maxHealth
    public void SetMaxHealthBarValue()
    {
        // Set the max and current health of the player health window
        maxHealth = playerObj.GetComponent<PlayerController>().maxHealth;
        // Set the playerHealthBar max and current values to maxHealth
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = maxHealth;
    }
    // Create a method to set the mana bar mana to the players current mana
    private void SetManaBarValue()
    {
        // Set currentMana to the players current mana
        currentMana = playerObj.GetComponent<PlayerController>().currentMana;
        // Set the playerManaBar value to currentMana
        playerManaBar.value = currentMana;
    }
    // Create a method to set the max value of the playerManaBar to the maxMana
    public void SetMaxManaBarValue()
    {
        // Set the max and current mana of the player mana window
        maxMana = playerObj.GetComponent<PlayerController>().maxMana;
        // Set the playerManaBar max and current values to maxMana
        playerManaBar.maxValue = maxMana;
        playerManaBar.value = maxMana;
    }
}
