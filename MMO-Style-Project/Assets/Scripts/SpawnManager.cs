using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Declare variable to determine if the spawn point is spawned or not
    public bool spawned;
    // Declare variable for an array of prefabs
    public GameObject[] enemies;
    // Declare variable for the spawned enemy object
    private GameObject enemy;
    // Declare variable for respawn time
    private float respawnTime = 10.0f;
    // Declare variables for random spawn roll and spawnNumber
    private int spawnRollLow = 1;
    private int spawnRollHigh = 100;
    private int spawnNumber;
    // Start is called before the first frame update
    void Start()
    {
        // Call the SpawnEnemy method
        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        // Call the IsSpawned method
        IsSpawned();
        // Start the RespawnEnemy Coroutine if there is no enemy at this spawn point
        if (spawned == false)
        {
            StartCoroutine("RespawnEnemy");
        }
    }
    // Create a method to spawn an enemy if none is currently spawned
    private void SpawnEnemy()
    {
        // Call the RandomizeEnemy method
        RandomizeEnemy();
        // Set the spawn position
        Vector3 spawnPos = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        // If there is no enemy spawn one at the spawnPos
        enemy = Instantiate(enemies[spawnNumber], spawnPos, transform.rotation);
        // Call the SetMaxHealth method
        enemy.GetComponent<EnemyController>().SetMaxHealth(); ;
        // If the spawn point is a patrol spawn point tag the enemy that spawns there as a patrol
        if (gameObject.CompareTag("Patrol Spawn Point"))
        {
            enemy.GetComponent<EnemyController>().isPatrol = true;
        }
    }
    // Create method to change spawned based on having an enemy spawned or not
    private void IsSpawned()
    {
        if (enemy == null)
        {
            spawned = false;
        } else
        {
            spawned = true;
        }
    }
    // Create method to determine which enemy should spawn based on spawnRoll
    private void RandomizeEnemy()
    {
        // Randomize the enemy that will spawn based on chance out of spawnRoll
        int spawnChance = Random.Range(spawnRollLow, spawnRollHigh);
        if (gameObject.CompareTag("Regular Spawn Point") || gameObject.CompareTag("Patrol Spawn Point"))
        {
            if (spawnChance > 75)
            {
                spawnNumber = 3;
            }
            else if (spawnChance > 50 & spawnChance < 76)
            {
                spawnNumber = 2;
            }
            else if (spawnChance > 25 & spawnChance < 51)
            {
                spawnNumber = 1;
            }
            else if (spawnChance < 26)
            {
                spawnNumber = 0;
            }
        } else if (gameObject.CompareTag("Rare Spawn Point"))
        {
            if (spawnChance > 90)
            {
                spawnNumber = 5;
            }
            else if (spawnChance > 80 & spawnChance < 91)
            {
                spawnNumber = 4;
            }
            else if (spawnChance > 60 & spawnChance < 81)
            {
                spawnNumber = 3;
            }
            else if (spawnChance > 40 & spawnChance < 61)
            {
                spawnNumber = 2;
            }
            else if (spawnChance > 20 & spawnChance < 41)
            {
                spawnNumber = 1;
            }
            else if (spawnChance < 21)
            {
                spawnNumber = 0;
            }
        }
        
    }
    // Create a Coroutine to spawn a new enemy
    IEnumerator RespawnEnemy()
    {
        // Wait for the respawnTime and then spawn a new enemy
        yield return new WaitForSeconds(respawnTime);
        // Call the SpawnEnemy method
        SpawnEnemy();
        StopCoroutine("RespawnEnemy");
    }


}
