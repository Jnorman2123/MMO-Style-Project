using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Declare variable for the player game object
    GameObject player;
    // Declare variable for the camera offset
    Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        // Set the player gameobject
        player = GameObject.FindGameObjectWithTag("Player");
        // Set the cameraOffset
        cameraOffset = new Vector3(0.0f, 1.3f, 0.45f);
    }

    // Update is called once per frame
    void Update()
    {
        // Set the position of the camera to an offset of the player position
        gameObject.transform.position = player.transform.position + cameraOffset;
    }
}
