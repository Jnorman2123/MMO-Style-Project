using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NameController : MonoBehaviour
{
    // Declare variable for the main camera
    private Camera mainCamera;
    // Declare variables for the canvas and text mesh pro objects
    public Canvas nameCanvas;
    public TextMeshProUGUI nameText;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Call the SetNameText method
        SetNameText();
        // Call the FaceCamera method
        FaceCamera();
    }
    // Create a method to set the nameText
    private void SetNameText()
    {
        if (transform.CompareTag("Player"))
        {
            nameText.text = transform.name;
        } else if (transform.CompareTag("Enemy"))
        {
            transform.name.Replace("(Clone)", "").Trim();
        }
        
    }
    // Create method to make the enemy name canvas always face the main camera
    private void FaceCamera()
    {
        nameCanvas.transform.LookAt(mainCamera.transform);
    }
}