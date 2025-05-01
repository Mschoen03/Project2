using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float baseSpeed = 2f;        // Starting speed
    public float maxSpeed = 6f;         // Max speed it can reach
    public float accelerationRate = 0.5f; // How fast it ramps up (units/sec)

    private float currentSpeed;
    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player is tagged 'Player'.");
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Gradually increase speed
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += accelerationRate * Time.fixedDeltaTime;
                if (currentSpeed > maxSpeed)
                    currentSpeed = maxSpeed;
            }

            // Move toward player
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 moveStep = direction * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveStep);
        }
    }
}
