using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 startPosition;
    private float xDistanceTraveled = 0f;
    private float yDistanceTraveled = 0f;
    private float startTime;

    public Animator bulletAnimation;

    void Start()
    {
        // Store the start position when the bullet is instantiated
        startPosition = transform.position;
        startTime = Time.time;

        // Start the bullet animation when it's fired
        bulletAnimation.SetBool("IsFired", true);
    }

    void Update()
    {
        // Calculate the time elapsed since the bullet was fired
        float elapsedTime = Time.time - startTime;

        // Calculate the distance traveled along the X and Y axes
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        xDistanceTraveled = Mathf.Abs(currentPosition.x - startPosition.x);
        yDistanceTraveled = Mathf.Abs(currentPosition.y - startPosition.y);

        // Calculate and display the speed of the bullet along X and Y axes in Unity's console
        float xSpeed = xDistanceTraveled / elapsedTime;
        float ySpeed = yDistanceTraveled / elapsedTime;

        // Debug.Log("X Speed: " + xSpeed + " units per second");
        // Debug.Log("Y Speed: " + ySpeed + " units per second");
    }
}
