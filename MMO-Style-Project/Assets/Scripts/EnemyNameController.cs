using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNameController : MonoBehaviour
{
    // Declare variable for the main camera
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the FaceCamera method
        FaceCamera();
    }
    // Create method to make the enemy name canvas always face the main camera
    private void FaceCamera()
    {
        transform.LookAt(mainCamera.transform);
    }
}
