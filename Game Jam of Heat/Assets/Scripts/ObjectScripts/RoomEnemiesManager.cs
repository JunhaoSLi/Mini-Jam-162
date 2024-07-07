using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemiesManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private List<GameObject> generatedEnemies = new List<GameObject>();

    void Start()
    {
        // partially copied from PositionWalls, maybe congregate shared values in Room.cs or otherwise
        float roomWidth = 16f; // Width of the room
        float roomHeight = 9f; // Height of the room
        float northWallThickness = 1.5f;
        float wallThickness = 0.3f;
        int numEnemiesToGenerate = 2;
        float enemyToCharPadding = 3;
        float enemyToWallPadding = 1;

        // Get the position of the Room object this script is attached to
        Vector3 roomPosition = transform.position;
        
        // Calculate bounds for distance from room 
        float lowerXBound = (-roomWidth / 2) + wallThickness + enemyToWallPadding; 
        float upperXBound = (roomWidth / 2) - wallThickness - enemyToWallPadding;
        float lowerYBound = (-roomHeight/ 2) + wallThickness + enemyToWallPadding;
        float upperYBound = (roomHeight / 2) - northWallThickness - enemyToWallPadding;


        for (int i = 0; i < numEnemiesToGenerate; i++)
        {
            bool enemyGenerated = false;
            while(!enemyGenerated)
            {
                // pick a random spawn point within our
                Vector2 candidateSpawnRelative = new Vector2(
                    Random.Range(lowerXBound, upperXBound),
                    Random.Range(lowerYBound, upperYBound)
                );
                Vector3 candidateSpawn =  roomPosition + (Vector3) candidateSpawnRelative;

                // Check if the spawn point is too close to the center to avoid spawning on the player which happens to be at the center
                // (find a better way to avoid spawning on the player later)
                if ((roomPosition - candidateSpawn).magnitude < enemyToCharPadding)
                {
                    continue;
                }

                // Check if the spawn point is too close to any already generated enemies
                bool skip = false;
                foreach (var enemy in generatedEnemies)
                {
                    if ((enemy.transform.position - candidateSpawn).magnitude < enemyToCharPadding)
                    {
                        skip = true;
                    }
                }

                if (!skip)
                {
                    // spawn point is good, generate the enemy
                    GameObject newEnemy = Instantiate(enemyPrefab, candidateSpawn, Quaternion.identity, transform);
                    generatedEnemies.Add(newEnemy);
                    newEnemy.SetActive(false);
                    enemyGenerated = true;
                }
            };
        }
    }

    public void ActivateEnemies()
    {
        foreach (var enemy in generatedEnemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }

    public void DeactivateEnemies()
    {
        foreach (var enemy in generatedEnemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);
            }
        }
    }
}
