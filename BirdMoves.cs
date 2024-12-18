using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the movement of bird objects in the game.
// The birds move in a random direction, periodically change direction, and are destroyed
// if they move off-screen. They also destroy any "Star" objects they collide with.

public class BirdMovement : MonoBehaviour
{
    public float speed = 15f;                     // Movement speed of the bird
    public float changeDirectionInterval = 2f;    // Time interval for changing direction
    private Vector3 direction;                    // Current movement direction of the bird

    // Initializes the bird's movement direction and sets up periodic direction changes
    void Start()
    {
        // Set an initial random direction for the bird (left or right on the x-axis)
        float randomDirection = Random.value > 0.5f ? 1f : -1f;
        direction = new Vector3(randomDirection, Random.Range(-1f, 1f), 0).normalized;

        // Flip the bird's sprite to match the initial movement direction if moving left
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        // Invoke a direction change at regular intervals
        InvokeRepeating("ChangeDirection", changeDirectionInterval, changeDirectionInterval);
    }

    // Updates the bird's position each frame
    void Update()
    {
        // Move the bird in its current direction
        transform.position += direction * speed * Time.deltaTime;

        // Check if the bird has moved off the screen boundaries
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPosition.x < -0.1f || screenPosition.x > 1.1f || screenPosition.y < -0.1f || screenPosition.y > 1.1f)
        {
            Destroy(gameObject);  // Destroy the bird if it goes off-screen
        }
    }

    // Randomly changes the bird's vertical direction
    private void ChangeDirection()
    {
        // Keep the same horizontal direction but change the vertical direction randomly
        direction = new Vector3(direction.x, Random.Range(-1f, 1f), 0).normalized;
    }

    // Detects collisions with objects tagged as "Star"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy the "Star" object upon collision
        if (other.CompareTag("Star"))
        {
            Destroy(other.gameObject);
        }
    }
}
