using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyDamage : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }
}
