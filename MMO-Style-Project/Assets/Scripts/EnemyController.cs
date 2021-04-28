using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemyController : MonoBehaviour
{
    // Declare variable for hate list dictionary
    public Dictionary<GameObject, int> hateList = new Dictionary<GameObject, int>();
    // Declare variable for the enemy target
    public GameObject enemyTarget;
    // Declare variable for is aggro
    public bool isAggro;
    // Declare variable for enemy is patrol
    public bool isPatrol;
    // Start is called before the first frame update
    void Start()
    {
        // Set isAggro to false
        isAggro = false;
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
    }
    
    // Create method to gain hate
    public void GainHate(int hate, GameObject attacker)
    {
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
    private void Aggro()
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
        if (hateList.Count > 0)
        {
            enemyTarget = hateList.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            Debug.Log(enemyTarget.name);
        } else
        {
            enemyTarget = null;
        } 
    }
}