using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ChatWindowController : MonoBehaviour
{
    // Declare variables for the chat log and chat entry text
    public TextMeshProUGUI chatLogText;
    public TextMeshProUGUI chatEntryText;
    // Declare variable for the player game object
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        chatLogText.text = "Welcome to the Adventures of Zidgog!";
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SetChatEntryText method
        SetChatEntryText();
    }
    // Create method to set the chat log text
    public void SetChatLogText(string message)
    {
        // Set the chat log text
        chatLogText.text += ("\r\n" + message);
    }
    // Create method to set the chat entry text
    private void SetChatEntryText()
    {
        // Set the chat entry text
        chatEntryText.text = "You type here to chat.";
    }
}
