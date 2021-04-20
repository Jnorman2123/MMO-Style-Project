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
    // Start is called before the first frame update
    void Start()
    {
        // Set the spawn rotation
        transform.Rotate(0.0f, -90.0f, 0.0f);
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
        // Set the spawn position
        Vector3 spawnPos = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        // If there is no enemy spawn one at the spawnPos
        enemy = Instantiate(enemies[0], spawnPos, transform.rotation);
        // Call the SetMaxHealth method
        enemy.GetComponent<EnemyController>().SetMaxHealth(); ;
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
