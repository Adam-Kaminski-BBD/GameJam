using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 0.5f; // Adjust this value to control the enemy's speed

    private Transform player;

    void Start()
    {
        StartCoroutine(UpdateClosestPlayer());
    }

    IEnumerator UpdateClosestPlayer()
    {
        while (true)
        {
            FindClosestPlayer();
            yield return new WaitForSeconds(5f); // Wait for 5 seconds before updating again
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        foreach (GameObject p in players)
        {
            float distance = Vector3.Distance(transform.position, p.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                player = p.transform;
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            Vector2 movement = direction * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }
}