using System.Collections;
using System.Collections.Generic;

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
        /*Get the enemy component of the object that was hit
        //Enemy enemy = hitInfo.GetComponent<Enemy>();

        //if the object that was hit has an enemy component
        if (enemy != null)
        {
            //call the take damage function of the enemy component
            enemy.TakeDamage(damage);
        } */

        if (hitInfo.gameObject.tag != "Player")
        {
           

            Destroy(gameObject);
        }

    }
}
