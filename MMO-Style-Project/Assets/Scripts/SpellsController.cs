using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    // Declare variable for casting
    public bool casting;
    // Start is called before the first frame update
    void Start()
    {
        // Set casting
        casting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Player casts Blast Of Fire when 2 is pressed and not currently casting
        if (Input.GetKeyDown(KeyCode.Alpha2) & !casting)
        {
            // Call CastBlastOfFire method
            GetComponent<BlastOfFireController>().CastBlastOfFire();
        }
        // Player casts Minor Healing when 3 is pressed and not currently casting
        if (Input.GetKeyDown(KeyCode.Alpha3) & !casting)
        {
            // Call the CastMinorHealing method
            GetComponent<MinorHealingController>().CastMinorHealing();
        }      
    }
}
