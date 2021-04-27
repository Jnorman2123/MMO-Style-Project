using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    // Declare variables for player current and max mana
    public int currentMana;
    public int maxMana;
    // Start is called before the first frame update
    void Start()
    {
        // Set the max and current mana
        maxMana = 100;
        currentMana = maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the UseMana method if the player presses the g key
        if (Input.GetKeyDown(KeyCode.G))
        {
            UseMana();
        }
    }
    // Create method to simulate using mana to test mana bar
    private void UseMana()
    {
        currentMana -= 10;
    }
}
