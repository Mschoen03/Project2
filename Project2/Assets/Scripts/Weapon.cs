using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject ProjectilePrefab;
    public Transform firePoint;
    public float fireforce = 20f;
     public AudioSource gunshotSound;


    public void Fire()
    {
        
        
        if (gunshotSound != null)
        {
            gunshotSound.Play();
        }
        else
        {
            Debug.LogWarning("Gunshot sound AudioSource is not assigned.");
        }


        if (ProjectilePrefab != null && firePoint != null)
        {
            Vector3 spawnPos = firePoint.position;
            spawnPos.z = 0f; // force Z = 0
            GameObject bullet = Instantiate(ProjectilePrefab, spawnPos, Quaternion.identity);

            // Calculate direction from firePoint to mouse
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mouseWorldPos - (Vector2)firePoint.position).normalized;

            // Apply velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = shootDirection * fireforce;
                
            }
            else
            {
                Debug.LogWarning("Bullet has no Rigidbody2D!");
            }

            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab or firePoint is not assigned.");
        }
    }
}
