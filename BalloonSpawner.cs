using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for spawning balloons in random positions within the screen area.
// The spawner creates a specified number of balloons, each with adjustable speed and growth intervals.

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;        // Reference to the balloon prefab to be spawned
    public int balloonCount = 3;            // Number of balloons to spawn
    public float spawnAreaPadding = 1f;     // Padding to keep balloons within screen bounds
    public float balloonSpeed = 5f;         // Initial speed for spawned balloons
    public float growInterval = 2f;         // Growth interval for spawned balloons

    // Spawns balloons when the game starts
    void Start()
    {
        SpawnBalloons();
    }

    // Spawns a specified number of balloons at random positions within the screen area
    void SpawnBalloons()
    {
        for (int i = 0; i < balloonCount; i++)
        {
            // Get a random spawn position within the screen area
            Vector3 spawnPosition = GetRandomScreenPosition();
            
            // Instantiate a new balloon at the random position
            GameObject balloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
 
            // Set the initial speed and growth interval of the balloon
            BalloonMoves balloonMove = balloon.GetComponent<BalloonMoves>();
            if (balloonMove != null)
            {
                balloonMove.speed = balloonSpeed;       // Assign speed from the spawner
                balloonMove.growInterval = growInterval; // Assign grow interval from the spawner
            }
        }
    }

    // Generates a random position within the screen boundaries, adjusted by padding
    Vector3 GetRandomScreenPosition()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Randomize X and Y positions within screen bounds, adjusted by padding
        float randomX = Random.Range(screenBottomLeft.x + spawnAreaPadding, screenTopRight.x - spawnAreaPadding);
        float randomY = Random.Range(screenBottomLeft.y + spawnAreaPadding, screenTopRight.y - spawnAreaPadding);

        return new Vector3(randomX, randomY, 0);  // Return the random spawn position
    }
}
