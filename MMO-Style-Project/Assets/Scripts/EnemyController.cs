using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Declare variable for the enemy character controller
    public CharacterController enemyController;
    // Declare variable for the enemy health
    public int enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetEnemyHealth method
        SetEnemyHealth();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Create method to set the health of the enemy based on its type
    private void SetEnemyHealth()
    {
        // Use a switch case statement to set the enemy health based on the enemy tag
        string tag = gameObject.tag;
        switch(tag)
        {
            case "Warrior":
                enemyHealth = 200;
                break;
            case "Rogue":
                enemyHealth = 175;
                break;
            case "Wizard":
                enemyHealth = 125;
                break;
            case "Cleric":
                enemyHealth = 150;
                break;
            case "Captain":
                enemyHealth = 250;
                break;
            case "Key Master":
                enemyHealth = 300;
                break;
        }
    }
}
