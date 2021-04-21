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
    // Declare variable for the player health bar slider
    public Slider playerHealthBar;
    // Declare variables for current and max health
    private int maxHealth;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        // Set the text of the player name window
        playerNameText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SetHealth method
        SetHealthBarValue();
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
}
