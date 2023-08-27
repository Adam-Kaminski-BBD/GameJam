using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private LineRenderer lineRenderer; // Reference to the LineRenderer component

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
            Debug.Log("Object Position (world): " + playerPosition);
            //Debug.Log("Mouse Click Position (screen): " + mouseClickPosScreen);
            Debug.Log("Mouse Click Position (world): " + mouseClickPosWorld);

            FireLine(playerPosition, mouseClickPosWorld);

            StartCoroutine(DisableLineRendererAfterDelay());
        }

    }

    void FireLine(Vector3 playerPosition, Vector3 clickPosition)
    {
        Vector3 extendedEnd = clickPosition + (clickPosition - playerPosition).normalized * 5.0f;
        //draw a line from playerPosition past clickPosition
        lineRenderer.SetPosition(0, playerPosition);
        lineRenderer.SetPosition(1, extendedEnd);

        // Enable the LineRenderer to make the line visible
        lineRenderer.enabled = true;

        // Optionally, you can disable the line after a short delay
        // StartCoroutine(DisableLineRendererAfterDelay());
    }

    IEnumerator DisableLineRendererAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay time as needed
        lineRenderer.enabled = false;
    }
}