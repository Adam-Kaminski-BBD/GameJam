using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamage : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public HealthBar healthBar;

    //enemy death animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Taken damage");
            TakeDamage(5);
        }
        if(col.gameObject.tag == "Wall")
        {
            //collide with wall
            
            
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(); // Call the Die method when health reaches zero.
        }
    }

    void Die()
    {
        // Trigger the "DieTrigger" parameter in the Animator to play the death animation.
        animator.SetTrigger("DeathTrigger");
        StartCoroutine(DestroyAfterAnimation());


    }

    // Update is called once per frame
   /* void Update()
    {
        if (currentHealth <= 0)
        {
            //animator.SetTrigger("DeathTrigger");
            Destroy(gameObject);
        }
    }*/

    // Method to destroy the enemy after the death animation has finished (if needed).
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(0.6f);

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
