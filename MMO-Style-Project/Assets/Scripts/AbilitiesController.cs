using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    // Declare variable for using ability
    public bool usingAbility;
    // Start is called before the first frame update
    void Start()
    {
        // Set usingAbility to false
        usingAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareTag("Player") & Input.GetKeyDown(KeyCode.Alpha1) & usingAbility == false)
        {
            // Call the TauntingStrike method
            GetComponent<TauntingStrikeController>().TauntingStrike();
        }
    }
}
