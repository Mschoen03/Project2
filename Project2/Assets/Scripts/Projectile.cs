using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Rigidbody component of the projectile
    private Rigidbody2D rb;

    // speed of the projectile with a default value of 20
    public float speed = 20f;

    //Damage of the projectile with a default value 20
    public int damage = 20;

    //impact effect of the projectile with value of 20
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        //get rigidbody component
        rb = GetComponent<Rigidbody2D>();

        // set the colocity of the projectile to fire to the right at the speed
       

    }

    //function called when the projectile collides with another object
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        UnityEngine.Debug.Log($"Bullet hit: {hitInfo.name} | Layer: {LayerMask.LayerToName(hitInfo.gameObject.layer)} | Tag: {hitInfo.tag}");


        // Try to get the zombie health component
        ZombieHealth zombie = hitInfo.GetComponentInParent<ZombieHealth>();
        if (zombie != null)
        {
            UnityEngine.Debug.Log(" ZombieHealth found! Applying damage.");
            zombie.TakeDamage(damage);
        }
        else
        {
            UnityEngine.Debug.Log(" ZombieHealth NOT found on: " + hitInfo.name);
        }

        if (!hitInfo.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
