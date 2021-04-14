using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    // Declare variable for target
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the MouseTarget method
        MouseTarget();
    }
    // Create method that targets the object the player left clicks on
    private void MouseTarget()
    {
        // When the mouse is clicked on an object target it and log its tag
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = (hit.transform.parent.gameObject);
            } else
            {
                target = null;
            }
        }
    }
}
