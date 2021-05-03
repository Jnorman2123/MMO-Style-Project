using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntingStrikeController : MonoBehaviour
{
    // Declare variable for chat ui window and combat message
    private GameObject chatUIWindow;
    private string combatMessage;
    // Start is called before the first frame update
    void Start()
    {
        // Set the chatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Create a method for the taunting strike ability
    public void TauntingStrike()
    {
        // Set usingAbility to true
        GetComponent<AbilitiesController>().usingAbility = true;
        // Set the combat message and log it to the chat window
        combatMessage = gameObject.name.Replace("(Clone)", "").Trim() + " prepares to use Taunting Strike!";
        chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
        // Set taunting strike to true
        GetComponent<CombatController>().isTauntingStrike = true;
    }
}
