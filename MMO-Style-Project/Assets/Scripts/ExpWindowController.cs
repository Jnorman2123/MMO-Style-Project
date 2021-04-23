using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ExpWindowController : MonoBehaviour
{
    // Declare variables level and exp bar window text
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI playerExpBarText;
    // Declare variable for the exp bar slider
    public Slider playerExpBar;
    // Declare variable for the player game object
    public GameObject player;
    // Declare variables for current and max exp values
    private int currentExpValue;
    private int maxExpValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SetExpBarValue method
        SetExpBarValue();
        // Call the SetLevelText method
        SetLevelText();
        // Call the SetExpText method
        SetExpBarText();
    }
    // Create a method to set the current value of the playerExpBar
    private void SetExpBarValue()
    {
        // Set currentExpValue to the players current exp
        currentExpValue = player.GetComponent<PlayerController>().currentExp;
        // Set the playerExpBar value to the currentExpValue
        playerExpBar.value = currentExpValue;
    }
    // Create a method to set the max value of the playerExpBar
    public void SetExpBarMaxValue()
    {
        // Set the max and current exp of the playerExpBar
        maxExpValue = player.GetComponent<PlayerController>().maxExp;
        // Set the playerExpBar max and current values
        playerExpBar.maxValue = maxExpValue;
        playerExpBar.value = currentExpValue;
    }
    // Create a method to set the text of the level window
    private void SetLevelText()
    {
        playerLevelText.text = player.GetComponent<PlayerController>().playerLevel.ToString();
    }
    // Create a method to set the text of the exp window
    private void SetExpBarText()
    {
        playerExpBarText.text = player.GetComponent<PlayerController>().currentExp.ToString() + "/" +
                                player.GetComponent<PlayerController>().maxExp.ToString();
    }
}
