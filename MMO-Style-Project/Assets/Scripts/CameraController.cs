using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Declare variable for the first person camera game object
    private GameObject firstPersonCamera;
    // Start is called before the first frame update
    void Start()
    {
        // Set the player gameobject
        firstPersonCamera = GameObject.FindGameObjectWithTag("FPC");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Use a late update
    private void LateUpdate()
    {
        // Set the position of the camera to an offset of the player position
        gameObject.transform.position = firstPersonCamera.transform.position;
        // Set the rotation of the camera to equal the rotation of the player
        gameObject.transform.rotation = firstPersonCamera.transform.rotation;
    }
}
