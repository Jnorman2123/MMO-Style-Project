using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerWindowController : MonoBehaviour
{
    // Declare variable for the player window ui text
    public TextMeshProUGUI playerWindowText;
    // Declare variable for player name
    private string playerName = "Zidgog";
    // Start is called before the first frame update
    void Start()
    {
        playerWindowText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
