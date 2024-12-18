using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the player character's movement and shooting actions.
// It allows movement using the horizontal and vertical axes, controls the facing direction,
// and shoots a projectile (Star) with a cooldown between shots.

public class PlayerControl : MonoBehaviour
{
    public float speed = 10f;                   // Movement speed of the player
    public GameObject starPrefab;               // Reference to the star projectile prefab
    public Transform firePoint;                 // Point from where the star is fired
    public float shootCooldown = 0.5f;          // Cooldown time between shots

    private Rigidbody2D rigidbody2D;            // Reference to the Rigidbody2D component
    private float x;                            // Horizontal input axis value
    private float y;                            // Vertical input axis value
    private int facingDirection = 1;            // Direction the player is facing (1 for right, -1 for left)
    private float lastShootTime = 1f;           // Tracks the last time the player shot

    // Initializes the rigidbody component and resets the shooting cooldown
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lastShootTime = -shootCooldown;  // Allows immediate shooting at start
    }

    // Updates player input and controls direction, movement, and shooting
    void Update()
    {
        // Capture horizontal and vertical input
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Update facing direction based on horizontal input
        if (x > 0)
        {
            facingDirection = 1;  // Facing right
            rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (x < 0)
        {
            facingDirection = -1; // Facing left
            rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Handle shooting action with cooldown
        if (Input.GetButtonDown("Fire1") && Time.time >= lastShootTime + shootCooldown)
        {
            ShootStar();
            lastShootTime = Time.time;  // Update last shoot time
        }

        // Call movement function to handle player movement
        Run();
    }

    // Moves the player based on input and speed
    void Run()
    {
        Vector3 movement = new Vector3(x, y, 0);
        rigidbody2D.transform.position += movement * speed * Time.deltaTime;
    }

    // Shoots a star projectile in the current facing direction
    void ShootStar()
    {
        // Instantiate a star at the fire point's position and rotation
        GameObject star = Instantiate(starPrefab, firePoint.position, firePoint.rotation);

        // Set the direction of the star to match the player's facing direction
        StarMoves starMoves = star.GetComponent<StarMoves>();
        if (starMoves != null)
        {
            starMoves.SetDirection(facingDirection);
        }
    }
}
