using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorController : MonoBehaviour
{
    // Declare variable for armor
    public int armor;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SetBaseArmor method
        SetBaseArmor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Declare method to set base armor
    private void SetBaseArmor()
    {
        // Set armor based on character type
        if (CompareTag("Player"))
        {
            armor = 5;
        }
        else if (CompareTag("Enemy"))
        {
            string name = gameObject.name.Replace("(Clone)", "").Trim();
            switch (name)
            {
                case "Warrior":
                    armor = 5;
                    break;
                case "Wizard":
                    armor = 2;
                    break;
                case "Rogue":
                    armor = 4;
                    break;
                case "Cleric":
                    armor = 3;
                    break;
                case "Captain":
                    armor = 5;
                    break;
                case "Key Master":
                    armor = 5;
                    break;
            }
        }
    }
}
