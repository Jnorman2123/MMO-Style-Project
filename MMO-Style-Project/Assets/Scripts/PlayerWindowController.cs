using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerWindowController : MonoBehaviour
{
    // Declare variable for the player window ui text
    public TextMeshProUGUI playerNameText;
    // Declare variables for the player health and mana bar text
    public TextMeshProUGUI playerHealthBarText;
    public TextMeshProUGUI playerManaBarText;
    // Declare variable for player name
    private string playerName = "Zidgog";
    // Declare variable for the player game object
    public GameObject player;
    // Declare variables for the player health and mana bar sliders
    public Slider playerHealthBar;
    public Slider playerManaBar;
    // Declare variables for current and max health and mana
    private int maxHealthValue;
    private int currentHealthValue;
    private int maxManaValue;
    private int currentManaValue;
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
        SetPlayerResourceBarValues();
        // Call the SetPlayerResourceBarText method
        SetPlayerResourceBarText();
    }
    // Create a method to set the health  and mana bar values to the players current health and mana
    private void SetPlayerResourceBarValues()
    {
        // Set currentHealth to the players current health
        currentHealthValue = player.GetComponent<HealthController>().currentHealth;
        // Set the playerHealthBar value to currentHealth
        playerHealthBar.value = currentHealthValue;
        // Set currentMana to the players current mana
        currentManaValue = player.GetComponent<ManaController>().currentMana;
        // Set the playerManaBar value to currentMana
        playerManaBar.value = currentManaValue;
    }
    // Create a method to set the max value of the player health and mana to current health and mana
    public void SetPlayerMaxResourceBarValues()
    {
        // Set the max and current health of the player health window
        maxHealthValue = player.GetComponent<HealthController>().maxHealth;
        // Set the playerHealthBar max and current values to maxHealth
        playerHealthBar.maxValue = maxHealthValue;
        playerHealthBar.value = maxHealthValue;
        // Set the max and current mana of the player mana window
        maxManaValue = player.GetComponent<ManaController>().maxMana;
        // Set the playerManaBar max and current values to maxMana
        playerManaBar.maxValue = maxManaValue;
        playerManaBar.value = maxManaValue;
    }
    // Create a method to set the player health and mana bar text
    private void SetPlayerResourceBarText()
    {
        playerHealthBarText.text = currentHealthValue + "/" + maxHealthValue;
        playerManaBarText.text = currentManaValue + "/" + maxManaValue;
    }
}
