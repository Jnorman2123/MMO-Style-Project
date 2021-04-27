using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TargetWindowController : MonoBehaviour
{
    // Declare variable for the target window ui text
    public TextMeshProUGUI targetNameText;
    // Declare variable for the player object and player target
    public GameObject player;
    private GameObject playerTarget;
    // Declare variable for target name
    private string targetName;
    // Declare variables for the target health and mana bar sliders
    public Slider targetHealthBar;
    public Slider targetManaBar;
    // Declare variables for the target health and mana bar text
    public TextMeshProUGUI targetHealthBarText;
    public TextMeshProUGUI targetManaBarText;
    // Declare variables for current and max health and mana 
    private int maxHealthValue;
    private int currentHealthValue;
    private int maxManaValue;
    private int currentManaValue;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Set the playerTarget
        playerTarget = player.GetComponent<TargetingController>().target;
        // Check that the playerTarget is not null
        if (playerTarget != null)
        {
            // Call the SetTargetText method
            SetTargetText();
            // Call the DeactivateResourceBars method
            DeactivateResourceBars();
            if (!playerTarget.CompareTag("Interactable"))
            {
                // Call the SetTargetMaxHealthBarValue
                SetTargetMaxHealthBarValue();
                // Call the SetTargetMaxManaBarValue
                SetTargetMaxManaBarValue();
                // Call the SetTargetHealthBarValue method
                SetTargetHealthBarValue();
                // Call the SetTargetManaBarValue method
                SetTargetManaBarValue();
                // Call the SetTargetHealthBarText method
                SetTargetHealthBarText();
                // Call the SetTargetManaBarText method
                SetTargetManaBarText();
            }
        } 
    }
    // Create a method to set the text value of the target ui window based on player target
    private void SetTargetText()
    {
        // Set the targetName to the playerTarget name
        targetName = playerTarget.name;
        // Set the targetNameText to the target name with (Clone) removed
        targetNameText.text = targetName.Replace("(Clone)", "").Trim();
    }
    // Create a method to deactivate the health and mana bars if the target is not a character 
    private void DeactivateResourceBars()
    {
        if (playerTarget.CompareTag("Interactable"))
        {
            targetHealthBar.gameObject.SetActive(false);
            targetManaBar.gameObject.SetActive(false);
        } else
        {
            targetHealthBar.gameObject.SetActive(true);
            targetManaBar.gameObject.SetActive(true);
        }
    }
    // Create a method to set the health bar health to the player targets current health
    private void SetTargetHealthBarValue()
    {
        if (playerTarget == player)
        {
            // Set currentHealth to the player targets current health
            currentHealthValue = player.GetComponent<HealthController>().currentHealth;  
        } else
        {
            // Set currentHealth to the player targets current health
            currentHealthValue = playerTarget.GetComponent<HealthController>().currentHealth;
        }
        // Set the targetHealthBar value to currentHealth
        targetHealthBar.value = currentHealthValue;
    }
    // Create a method to set the max value of the targetHealthBar to the maxHealth
    public void SetTargetMaxHealthBarValue()
    {
        if (playerTarget == player)
        {
            // Set maxHealth to the player targets max health
            maxHealthValue = player.GetComponent<HealthController>().maxHealth;
        } else
        {
            // Set maxHealthValue to the targets max health
            maxHealthValue = playerTarget.GetComponent<HealthController>().maxHealth;
        }
        // Set the targetHealthBar max and current values to maxHealthValue
        targetHealthBar.maxValue = maxHealthValue;
        targetHealthBar.value = maxHealthValue;
    }
    // Create a method to set the mana bar value to the player targets current mana
    private void SetTargetManaBarValue()
    {
        if (playerTarget == player)
        {
            // Set currentMana to the players current mana
            currentManaValue = playerTarget.GetComponent<ManaController>().currentMana;
        } else
        {
            // Set currentMana to the player target current mana
            currentManaValue = playerTarget.GetComponent<ManaController>().currentMana;
        }
        // Set the targetManaBar value to currentMana
        targetManaBar.value = currentManaValue;
    }
    // Create a method to set the max value of the targetManaBar to the maxMana
    public void SetTargetMaxManaBarValue()
    {
        if (playerTarget == player)
        {
            // Set maxMana to the player targets max mana
            maxManaValue = player.GetComponent<ManaController>().maxMana;
        } else
        {
            // Set the max and current mana of the player target mana window
            maxManaValue = playerTarget.GetComponent<ManaController>().maxMana;
        }
        // Set the playerManaBar max and current values to maxMana
        targetManaBar.maxValue = maxManaValue;
        targetManaBar.value = maxManaValue;
    }
    // Create a method to set the target health bar text
    private void SetTargetHealthBarText()
    {
        targetHealthBarText.text = currentHealthValue + "/" + maxHealthValue;
    }
    // Create a method to set the target mana bar text
    private void SetTargetManaBarText()
    {
        targetManaBarText.text = currentManaValue + "/" + maxManaValue;
    }
}
