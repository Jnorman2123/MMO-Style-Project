using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TargetWindowController : MonoBehaviour
{
    // Declare variable for the target window ui text
    public TextMeshProUGUI targetNameText;
    // Declare variable for the player object and player target
    public GameObject player;
    private GameObject playerTarget;
    // Declare variable for target name
    private string targetName;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Set the playerTarget
        playerTarget = player.GetComponent<PlayerTargeting>().target;
        if (playerTarget != null)
        {
            targetName = playerTarget.name;
        } else
        {
            targetName = "";
        }
        targetNameText.text = targetName.Replace("(Clone)", "").Trim();
    }
    
}
