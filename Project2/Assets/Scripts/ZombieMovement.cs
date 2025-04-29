using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public float moveSpeed = 2f; // How fast the zombie moves
    private Transform player;    // Player's transform

    void Start()
    {
        // Find the player by tag
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

    void Update()
    {
        if (player != null)
        {
            // Move toward the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
