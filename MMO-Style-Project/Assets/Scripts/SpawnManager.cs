using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Declare variable to determine if the spawn point is spawned or not
    public bool spawned;
    // Declare variable for an array of prefabs
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        // Spawned to false at the start of scene
        spawned = false;
        // Set the spawn rotation
        transform.Rotate(0.0f, -90.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Call the IsSpawned method
        IsSpawned();
        // Call the SpawnEnemy method
        SpawnEnemy();
    }
    // Create a method to spawn an enemy if none is currently spawned
    private void SpawnEnemy()
    {
        // Set the spawn position
        Vector3 spawnPos = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        if (spawned == false)
        {
            Debug.Log("Spawn Enemy");
            Instantiate(enemy, spawnPos, transform.rotation);
            // Call the SetEnemyHealth method
            enemy.GetComponent<EnemyController>().SetMaxHealth(); ;
            spawned = !spawned;
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
}
