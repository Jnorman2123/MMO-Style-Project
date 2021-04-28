using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare variables for player experience and player lvl
    public int currentExp;
    public int maxExp;
    public int playerLevel;
    // Declare variables for the player ui window, exp ui window, chat ui window. and player spawn point
    public GameObject playerUIWindow;
    public GameObject expUIWindow;
    public GameObject chatUIwindow;
    public GameObject playerSpawnPoint;
    // Declare variable for the player respawn pos
    private Vector3 respawnPos;
    private Quaternion respawnRot;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current exp and player level
        maxExp = 100;
        currentExp = 0;
        playerLevel = 1;
        // Set the respawnPos
        respawnPos = playerSpawnPoint.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        respawnRot = playerSpawnPoint.transform.rotation;
        // Call the SetMaxHealthManaBarValue method
        playerUIWindow.GetComponent<PlayerWindowController>().SetPlayerMaxResourceBarValues();
        // Call the SetExpBarMaxValue method
        expUIWindow.GetComponent<ExpWindowController>().SetExpBarMaxValue();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the LevelUp method
        LevelUp();
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
            maxExp += Mathf.RoundToInt(maxExp * 1.5f);
            currentExp = rollOverExp;
            GetComponent<HealthController>().currentHealth = GetComponent<HealthController>().maxHealth;
            string levelUpMessage = "Congratulations you are now level " + playerLevel + "!";
            // Display a message to show the player gained a level
            chatUIwindow.GetComponent<ChatWindowController>().SetChatLogText(levelUpMessage);
        }
    } 
    // Create a method to kill the player when it reaches zero health
    public void PlayerDeath()
    {
        // Player loses experience on death
        currentExp -= 20;
        // Return the player to the respawn position
        transform.position = respawnPos;
        transform.rotation = respawnRot;
    }
}
