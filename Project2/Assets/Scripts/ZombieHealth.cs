using System.Collections;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int health = 100;
    private ZombieDisplayBar healthBar;

    private void Start()
    {
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
        FindObjectOfType<WaveController>()?.DeregisterZombie();
        Destroy(gameObject);
    }
}
