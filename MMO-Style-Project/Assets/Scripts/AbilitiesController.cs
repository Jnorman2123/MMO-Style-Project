using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
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
        // Call the TauntingStrike method
        TauntingStrike();
    }
    // Create a method for the taunting strike ability
    public void TauntingStrike()
    {
        if (CompareTag("Player") & Input.GetKeyDown(KeyCode.Alpha1)
            & GetComponent<CombatController>().isTauntingStrike == false)
        {
            // Set the combat message and log it to the chat window
            combatMessage = gameObject.name.Replace("(Clone)", "").Trim() + " prepares to use Taunting Strike!";
            chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(combatMessage);
            // Set taunting strike to true
            GetComponent<CombatController>().isTauntingStrike = true;
        }
    }
}
