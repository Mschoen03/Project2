using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int spikeDamage = 10; // Damage the spike will deal

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has a PlayerHealth component
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        ZombieHealth zombieHealth = other.GetComponent<ZombieHealth>();

        if (playerHealth != null)
        {
            Debug.Log("Player touched spike!");
            playerHealth.TakeDamage(spikeDamage);
        }

        if (zombieHealth != null)
        {
            Debug.Log("Zombie touched spike!");
            zombieHealth.TakeDamage(spikeDamage);
        }

        Debug.Log("Something hit the spike: " + other.name);
    }

    void Start()
    {
    }

    void Update()
    {
    }
}
