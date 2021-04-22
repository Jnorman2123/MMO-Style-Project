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
        SetPlayerHealthBarValue();
        // Call the SetManaBarValue method
        SetPlayerManaBarValue();
        // Call the SetPlayerHealthBarText method
        SetPlayerHealthBarText();
        // Call the SetPlayerManaBarText method
        SetPlayerManaBarText();
    }
    // Create a method to set the health bar health to the players current health
    private void SetPlayerHealthBarValue()
    {
        // Set currentHealth to the players current health
        currentHealthValue = player.GetComponent<PlayerController>().currentHealth;
        // Set the playerHealthBar value to currentHealth
        playerHealthBar.value = currentHealthValue;
    }
    // Create a method to set the max value of the playerHealthBar to the maxHealth
    public void SetPlayerMaxHealthBarValue()
    {
        // Set the max and current health of the player health window
        maxHealthValue = player.GetComponent<PlayerController>().maxHealth;
        // Set the playerHealthBar max and current values to maxHealth
        playerHealthBar.maxValue = maxHealthValue;
        playerHealthBar.value = maxHealthValue;
    }
    // Create a method to set the mana bar mana to the players current mana
    private void SetPlayerManaBarValue()
    {
        // Set currentMana to the players current mana
        currentManaValue = player.GetComponent<PlayerController>().currentMana;
        // Set the playerManaBar value to currentMana
        playerManaBar.value = currentManaValue;
    }
    // Create a method to set the max value of the playerManaBar to the maxMana
    public void SetPlayerMaxManaBarValue()
    {
        // Set the max and current mana of the player mana window
        maxManaValue = player.GetComponent<PlayerController>().maxMana;
        // Set the playerManaBar max and current values to maxMana
        playerManaBar.maxValue = maxManaValue;
        playerManaBar.value = maxManaValue;
    }
    // Create a method to set the player health bar text
    private void SetPlayerHealthBarText()
    {
        playerHealthBarText.text = currentHealthValue + "/" + maxHealthValue;
    }
    // Create a method to set the player mana bar text
    private void SetPlayerManaBarText()
    {
        playerManaBarText.text = currentManaValue + "/" + maxManaValue;
    }
}
