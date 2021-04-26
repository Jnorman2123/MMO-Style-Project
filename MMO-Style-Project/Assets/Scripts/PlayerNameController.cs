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
    // Declare variable for the main camera
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Set the mainCamera
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the ActivatePlayerName method
        ActivatePlayerName();
        // Call the faceCamera method
        FaceCamera();
        
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
    // Create a method to face the camera
    private void FaceCamera()
    {
        transform.LookAt(mainCamera.transform);
    }
}
