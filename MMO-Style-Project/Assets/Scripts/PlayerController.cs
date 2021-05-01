using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare variables for player experience and player lvl
    public int currentExp;
    public int maxExp;
    public int playerLevel;
    // Declare variable for alive and respawn timer
    public bool alive;
    private float respawnTimer = 1.0f;
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
        // Set the alive variable
        alive = true;
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
        // Start the PlayerDeath coroutine if the players health drops to zero
        if (GetComponent<HealthController>().currentHealth <= 0)
        {
            StartCoroutine("PlayerDeath");
        }
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
    IEnumerator PlayerDeath()
    {
        GetComponent<CombatController>().autoAttacking = false;
        // Set alive to false
        alive = false;
        // Return the player to the respawn position
        transform.position = respawnPos;
        transform.rotation = respawnRot;
        // Wait for respawn timer and set to alive again
        yield return new WaitForSeconds(respawnTimer);
        alive = true;
        // Player loses experience on death
        currentExp -= 20;
        GetComponent<HealthController>().currentHealth = GetComponent<HealthController>().maxHealth / 2;
        StopCoroutine("PlayerDeath");
    }
}
