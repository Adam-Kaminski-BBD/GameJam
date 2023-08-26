using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f; // Adjust this value to control the enemy's speed

    private Transform player;
    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
         // Move towards the player
        Vector2 direction = (player.position - transform.position).normalized;
        Vector2 movement = direction * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
