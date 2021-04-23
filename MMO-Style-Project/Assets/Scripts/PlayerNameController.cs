using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerNameController : MonoBehaviour
{
    // Declare variable for the player text mesh pro
    public TextMeshProUGUI playerName;
    // Declare variable for the player game object
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Call the ActivatePlayerName method
        ActivatePlayerName();
        
    }
    // Create a method to activate the player name when the player is targeted
    private void ActivatePlayerName()
    {
        if (player.GetComponent<PlayerTargeting>().target == player)
        {
            playerName.text = player.name;
        }
        else
        {
            playerName.text = "";
        }
    }
}
