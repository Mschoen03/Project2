using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    public int attackDamage = 10;

    private float attackTimer = 0f;
    private Transform player;
    private Animator animator;
    private ZombieMovement zombieMovement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        zombieMovement = GetComponent<ZombieMovement>();
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && attackTimer <= 0f)
        {
            // Trigger attack animation
            animator.SetTrigger("AttackDown");

            // Stop movement briefly (optional)
            if (zombieMovement != null)
                zombieMovement.enabled = false;

            // Damage player
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(attackDamage);

            attackTimer = attackCooldown;

            // Resume movement after short delay
            StartCoroutine(ResumeMovement(0.5f));
        }
    }

    IEnumerator ResumeMovement(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (zombieMovement != null)
            zombieMovement.enabled = true;
    }
}
