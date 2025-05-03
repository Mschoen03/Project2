using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;

//using System.Diagnostics;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 100;

    private ZombieDisplayBar healthBar;

    private ZombieGameManager zombieGameManager;

    // Start is called before the first frame update
    private void Start()
    {
        zombieGameManager = FindObjectOfType<ZombieGameManager>();
        healthBar = GetComponentInChildren<ZombieDisplayBar>();

        if (healthBar == null)
        {
            Debug.LogError("HealthBar (DisplayBar script) not found");
            return;
        }

        healthBar.SetMaxValue(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        Debug.Log("Zombie took damage: " + damage);

        healthBar.SetValue(health);
        
        if (health <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
         if (zombieGameManager != null)
        {
        
            zombieGameManager.ZombieDied();
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
