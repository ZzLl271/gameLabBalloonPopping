using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the behavior of a "Star" projectile in the game.
// The star moves across the screen in a specified direction, checks for screen boundaries,
// and destroys itself when it collides with certain objects or moves off-screen.

public class StarMoves : MonoBehaviour
{
    public float moveSpeed = 20f;    // Speed at which the star moves
    private int direction = 1;       // Direction of movement (1 for right, -1 for left)

    // Updates the star's position each frame
    void Update()
    {
        // Move the star horizontally based on the direction and speed
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // Check if the star has moved off the screen
        float screenPositionX = Camera.main.WorldToViewportPoint(transform.position).x;
        if (screenPositionX > 1 || screenPositionX < 0)
        {
            Destroy(gameObject);  // Destroy the star if it goes off-screen
        }
    }

    // Sets the direction of the star and adjusts its scale accordingly
    public void SetDirection(int dir)
    {
        direction = dir;  // Set the movement direction (1 for right, -1 for left)

        // Flip the star sprite based on the direction of movement
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }

    // Detects collisions with objects tagged as "Cloud"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the star collides with an object tagged "Cloud"
        if (other.CompareTag("Cloud"))
        {
            Destroy(gameObject);  // Destroy the star upon collision with a cloud
        }
    }
}
