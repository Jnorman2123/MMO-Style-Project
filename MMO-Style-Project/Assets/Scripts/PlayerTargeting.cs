using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    // Declare variable for target
    public GameObject target;
    // Declare variable for the target ui window
    public GameObject targetUIWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the MouseTarget method
        MouseTarget();
        // Call the ClearTarget method
        ClearTarget();
        // Call the ActivateTargetWindow method
        ActivateTargetWindow();
    }
    // Create method that targets the object the player left clicks on
    private void MouseTarget()
    {
        // When the mouse is clicked on an object target it and log its tag
        if (Input.GetMouseButtonDown(0))
        {
            // Set the layer mask to the targetable layer only
            int layerMask = (1 << 8);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                if (!hit.transform.gameObject.CompareTag("Environment"))
                {
                    target = (hit.transform.parent.gameObject);
                    Debug.Log("Targeting " + target.name);
                    if (!target.CompareTag("Interactable"))
                    {
                        // Call the SetTargetMaxHealthBarValue
                        targetUIWindow.GetComponent<TargetWindowController>().SetTargetMaxHealthBarValue();
                        // Call the SetTargetMaxManaBarValue
                        targetUIWindow.GetComponent<TargetWindowController>().SetTargetMaxManaBarValue();
                    }
                }
                else
                {
                    target = null;
                }
            } 
        }
    }
    // Create method to clear the target
    private void ClearTarget()
    {
        if (target != null & Input.GetKeyDown("escape"))
        {
            target = null;
        }
    }
    // Create a method to only set the target window to active if the player has a valid target
    private void ActivateTargetWindow()
    {

        // Set the window to inactive if there is no player target and active when player has a valid target
        if (target != null)
        {
            targetUIWindow.gameObject.SetActive(true);
        }
        else
        {
            targetUIWindow.gameObject.SetActive(false);
        }
    }
}
