using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Call the TauntingStrike method
        TauntingStrike();
    }
    // Create a method for the taunting strike ability
    public void TauntingStrike()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<CombatController>().isTauntingStrike = true;
        }
    }
}
