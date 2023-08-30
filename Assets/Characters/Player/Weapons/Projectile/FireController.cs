using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private Vector3 startPosition;
    private float distanceTraveled = 0f;

    void Start()
    {
        // Store the start position when the bullet is instantiated
        startPosition = transform.position;
    }

    void Update()
    {
        distanceTraveled = Vector3.Distance(startPosition, transform.position);

        if (distanceTraveled >= 0.5f)
        {
            // Perform your desired action when the distance reaches 5f
            // For example, destroy the bullet
            Destroy(gameObject);
        }
    }
}
