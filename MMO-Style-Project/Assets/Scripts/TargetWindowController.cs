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
        playerTarget = player.GetComponent<PlayerTargeting>().target;
        // Check that the playerTarget is not null
        if (playerTarget != null)
        {
            // Call the SetTargetText method
            SetTargetText();
            // Call the DeactivateResourceBars method
            DeactivateResourceBars();
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
    // Create a method to set the max value of the targetHealthBar to the maxHealth
    private void SetTargetMaxHealthBarValue()
    {
        // Set currentHealthValue to the targets current health
        currentHealthValue = playerTarget.GetComponent<EnemyController>().currentHealth;
    }
}
