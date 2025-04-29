using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took damage! Current HP: " + currentHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Player died!");
        // Destroy(gameObject); // Uncomment to remove player on death
        // Add death animation, respawn, game over screen, etc.
    }


}
