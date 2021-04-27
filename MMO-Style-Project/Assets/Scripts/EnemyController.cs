using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Declare variable for hate and is aggro
    public int hate;
    public bool isAggro;
    // Declare variable for enemy is patrol
    public bool isPatrol;
    // Start is called before the first frame update
    void Start()
    {
        // Set hate to 0 and isAggro to false
        hate = 0;
        isAggro = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Call the Death method
        Death();
        // Call the Aggro method
        Aggro();
    }
    
    // Create method to gain hate
    public void GainHate(int damage)
    {
        // Increase hate by the damage taken
        hate += damage;
    }
    // Create a method to destroy to object when health reaches zero
    private void Death()
    {
        // Check to see if health is equal or less than zero and destroy to the object
        if (transform.GetComponent<HealthController>().currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Create a method to make the enemy aggro if he receives a hate gaining action
    private void Aggro()
    {
        if (hate > 0)
        {
            isAggro = true;
        } else
        {
            isAggro = false;
        }
    }
}
