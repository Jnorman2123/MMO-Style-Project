using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class ChatWindowController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Declare variables for the chat log and chat entry text
    public TextMeshProUGUI chatLogText;
    public TextMeshProUGUI chatEntryText;
    // Declare variable for the player game object
    public GameObject player;
    // Declare variable to determine if the mouse is hovering over the chat window
    public bool mouseOver;
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
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        Debug.Log("mouse is over the chat window");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        Debug.Log("mouse is not over the chat window");
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
