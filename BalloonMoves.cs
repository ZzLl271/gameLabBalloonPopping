using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the behavior of a balloon in the game.
// It handles the balloon's movement, growth, collision detection, and destruction.

public class BalloonMoves : MonoBehaviour
{
    public float speed = 15f;             // Speed at which the balloon moves
    private Vector3 direction;            // Movement direction of the balloon
    private Vector3 originalScale;        // Stores the initial scale of the balloon
    public float growFactor = 1.2f;       // Factor by which the balloon grows each interval
    public float growInterval = 2f;       // Time interval in seconds between each growth stage
    public int maxGrowthStages = 10;      // Maximum number of growth stages before balloon is destroyed
    public AudioClip collisionSound;      // Sound to play when balloon collides with a "Star"
    private int growthStage = 0;          // Current growth stage of the balloon
    private GameManager gameManager;      // Reference to the GameManager for score and balloon tracking

    // Initializes balloon movement, growth behavior, and registers it with the GameManager
    void Start()
    {
        // Set a random movement direction
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        originalScale = transform.localScale;

        // Start the growth process, which will be repeated at the specified interval
        InvokeRepeating("GrowBalloon", growInterval, growInterval);

        // Register this balloon with the GameManager to track active balloons
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.AddBalloon();
        }
    }

    // Updates the balloon's position each frame
    void Update()
    {
        // Move the balloon based on its direction and speed
        transform.position += direction * speed * Time.deltaTime;

        // Check if the balloon has reached screen boundaries and reflect its direction if necessary
        CheckScreenBounds();
    }

    // Reverses direction when the balloon reaches the screen edges
    private void CheckScreenBounds()
    {
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Reverse direction on horizontal boundaries
        if (screenPosition.x < 0 || screenPosition.x > 1)
        {
            direction.x = -direction.x;
            transform.localScale = new Vector3(-transform.localScale.x, originalScale.y, originalScale.z);
        }

        // Reverse direction on vertical boundaries
        if (screenPosition.y < 0 || screenPosition.y > 1)
        {
            direction.y = -direction.y;
            transform.localScale = new Vector3(transform.localScale.x, originalScale.y, originalScale.z); 
        }
    }

    // Handles balloon growth at each interval and destroys the balloon if it reaches max size
    void GrowBalloon()
    {
        growthStage++;
        transform.localScale *= growFactor;

        // Stop growth and destroy the balloon if it reaches the maximum growth stage
        if (growthStage >= maxGrowthStages)
        {
            CancelInvoke("GrowBalloon"); // Stop further growth
            Destroy(gameObject);         // Destroy the balloon
        }
    }

    // Detects collisions with "Star" objects, awards points, plays sound, and destroys both objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Star" tag
        if (other.CompareTag("Star"))
        {
            // Play collision sound at the balloon's location
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // Add score based on growth stage if balloon is not at max growth stage
            if (growthStage < maxGrowthStages && gameManager != null)
            {
                gameManager.AddScore(growthStage);
            }

            Destroy(other.gameObject);  // Destroy the "Star" object on impact
            Destroy(gameObject);        // Destroy the balloon on impact
        }
    }

    // Called automatically when the balloon is destroyed to notify the GameManager
    private void OnDestroy()
    {
        // Notify the GameManager that a balloon was destroyed
        if (gameManager != null)
        {
            gameManager.BalloonDestroyed();
        }
    }
}
