using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellsController : MonoBehaviour
{
    // Declare a variable for casting
    public bool casting;
    // Declare variable for name
    private string enemyName;
    // Start is called before the first frame update
    void Start()
    {
        // Set casting to false
        casting = false;
        // Set enemyName
        enemyName = gameObject.name.Replace("(Clone)", "").Trim();
    }

    // Update is called once per frame
    void Update()
    {
        // Enemy casts Blast of Fire if it is a wizard and agro and has enough mana
        if (enemyName == "Wizard" & GetComponent<EnemyController>().isAggro & 
            GetComponent<ManaController>().currentMana > GetComponent<BlastOfFireController>().manaCost
            & !casting)
        {
            // Call CastBlastOfFire method
            GetComponent<BlastOfFireController>().CastBlastOfFire();
        }

    }
}
