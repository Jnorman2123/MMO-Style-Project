using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemyController : MonoBehaviour
{
    // Declare variable for hate list dictionary
    public Dictionary<GameObject, int> hateList = new Dictionary<GameObject, int>();
    // Declare variable for the chat ui window
    private GameObject chatUIWindow;
    // Declare variable for the enemy target and previous target
    public GameObject enemyTarget;
    private GameObject previousTarget;
    // Declare variable for is aggro
    public bool isAggro;
    // Declare variable for enemy is patrol
    public bool isPatrol;
    // Start is called before the first frame update
    void Start()
    {
        // Set isAggro to false
        isAggro = false;
        // Set the chatUIWindow
        chatUIWindow = GameObject.Find("Chat UI Window");
    }
    // Update is called once per frame
    void Update()
    {
        // Call the Death method
        Death();
        // Call the Aggro method
        Aggro();
        // Call the SetTarget method
        SetTarget();
        // Call the RemoveDeadKeys method
        RemoveDeadKeys();
    }
    
    // Create method to gain hate
    public void GainHate(int hate, GameObject attacker)
    {
        // Set the hate gain based on taunting strike or not
        if (attacker.GetComponent<CombatController>().isTauntingStrike)
        {
            hate *= 2;
            attacker.GetComponent<CombatController>().isTauntingStrike = false;
        }
        // If the attacker doesn't exist in the hate list add it otherwise just add the hate to the attacker
        if (hateList.ContainsKey(attacker))
        {
            hateList[attacker] += hate;
        } else
        {
            hateList.Add(attacker, hate);
        }
    }
    // Create a method to destroy to object when health reaches zero
    private void Death()
    {
        // Check to see if health is equal or less than zero and destroy to the object
        if (transform.GetComponent<HealthController>().currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Create a method to make the enemy aggro if he receives a hate gaining action
    public void Aggro()
    {
        if (hateList.Count > 0)
        {
            isAggro = true;
        } else
        {
            isAggro = false;
        }
    }
    // Create a method to set the enemyTarget
    private void SetTarget()
    {
        previousTarget = enemyTarget;
        if (hateList.Count > 0)
        {
            enemyTarget = hateList.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            if (previousTarget != enemyTarget)
            {
                string aggroMessage = "You will pay for that " + enemyTarget.name + "!";
                chatUIWindow.GetComponent<ChatWindowController>().SetChatLogText(aggroMessage);
            }
        } else
        {
            enemyTarget = null;
        } 
    }
    // Create a method to remove a key from the hateList if the object is dead
    private void RemoveDeadKeys()
    {
        foreach (GameObject key in hateList.Keys)
        {
            if (key.CompareTag("Player") & !key.GetComponent<PlayerController>().alive)
            {
                hateList.Remove(key);
            }
        }
    }
}