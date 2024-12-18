using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the collision behavior for cloud objects in the game.
// When a "Star" object collides with the cloud, the star will be destroyed.

public class Cloud : MonoBehaviour
{
    // Called when another collider makes contact with the collider attached to the cloud object.
    // This function handles destroying any object tagged as "Star" on collision.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the tag "Star"
        if (collision.gameObject.CompareTag("Star"))
        {
            // Destroy the star object on impact with the cloud
            Destroy(collision.gameObject);
        }
    }
}
