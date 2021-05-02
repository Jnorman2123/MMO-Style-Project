using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistanceController : MonoBehaviour
{
    // Declare variable for resistance
    public int resistance;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetResistance method
        SetResistance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Create a method to set resistance based on willpower
    private void SetResistance()
    {
        resistance = Mathf.RoundToInt(GetComponent<StatsController>().willpower / 10);
    }
}
