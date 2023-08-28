using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private LineRenderer lineRenderer; // Reference to the LineRenderer component
    public GameObject bulletPrefab; // Reference to the bullet prefab named "Bullet"
    public float bulletSpeed = 5f; // Hardcoded constant speed of the fired bullet

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Two points for start and end of the line
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = transform.position;

        // Check if the left mouse button (Fire1) is clicked
        if (Input.GetMouseButtonDown(0)) // 0 corresponds to the left mouse button
        {
            // Get the mouse click position in screen coordinates
            Vector3 mouseClickPosScreen = Input.mousePosition;

            // Get the mouse click position in world coordinates
            Vector3 mouseClickPosWorld = Camera.main.ScreenToWorldPoint(mouseClickPosScreen);
            // Print the positions to the console

            //FireLine(playerPosition, mouseClickPosWorld);
            GetSetDistance(playerPosition, mouseClickPosWorld);
            FireBullet(mouseClickPosWorld);

            StartCoroutine(DisableLineRendererAfterDelay());
        }
    }

    void GetSetDistance(Vector3 playerPosition, Vector3 mouseClickPosWorld)
    {
        float dist = Vector3.Distance(playerPosition, mouseClickPosWorld);

        Debug.Log(dist);
    }

    void FireLine(Vector3 playerPosition, Vector3 clickPosition)
    {
        // Calculate the direction from the player to the click position and normalize it
        Vector3 direction = (clickPosition - playerPosition).normalized;

        // Define the desired length of the line
        float lineLength = 5.0f; // Fixed length of 5 units

        // Calculate the endpoint of the line based on the direction and desired length
        Vector3 lineEndpoint = playerPosition + direction * lineLength;

        Debug.Log("Player" + playerPosition);
        Debug.Log("click" + clickPosition);
        Debug.Log("5f away" + lineEndpoint);

        // Draw a line from playerPosition to the calculated endpoint
        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, lineEndpoint);

        // Enable the LineRenderer to make the line visible
        lineRenderer.enabled = true;
    }



    void FireBullet(Vector3 targetPosition)
    {
        // Create a new instance of the bullet at the player's position
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Calculate the direction from the player to the target position and normalize it
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.z = 0;

        // Get the bullet's Rigidbody2D and set the velocity to the constant speed in the calculated direction
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.velocity = direction * bulletSpeed; // Bullet speed remains constant
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 5f);
        //Debug.Log("VELOCITY"+rb.velocity);
        //Debug.Log("DIR"+direction.magnitude);

    }

    IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed
        lineRenderer.enabled = false;
    }
}
