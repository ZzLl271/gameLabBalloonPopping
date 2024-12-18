using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for spawning bird objects at regular intervals,
// randomly placing them on either the left or right edge of the screen at varying vertical positions.

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;        // Reference to the bird prefab to be spawned
    public float spawnInterval = 1f;     // Time interval in seconds between bird spawns
    public float spawnYRange = 5f;       // Range for random Y position adjustment

    // Starts the repeating bird spawn process at the specified interval
    void Start()
    {
        InvokeRepeating("SpawnBird", spawnInterval, spawnInterval);
    }

    // Spawns a bird at a random position along the screen's left or right edge
    void SpawnBird()
    {
        // Get the left and right edges of the screen in world coordinates
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0));

        // Randomly select either the left or right edge for the bird spawn position
        float spawnX = Random.value > 0.5f ? leftEdge.x - 1 : rightEdge.x + 1;

        // Generate a random Y position within the specified range from the screen center
        float spawnY = Random.Range(leftEdge.y - spawnYRange, rightEdge.y + spawnYRange);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        // Instantiate the bird at the calculated spawn position
        GameObject bird = Instantiate(birdPrefab, spawnPosition, Quaternion.identity);
    }
}
