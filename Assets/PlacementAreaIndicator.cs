using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAreaIndicator : MonoBehaviour
{
    public float placementRadius = 0.1f; // Radius around the player for placement
    public Color indicatorColor = Color.green;
    public int segments = 10; // Number of segments to create a smooth circle
    public float lineWidth = 0.01f; // Width of the circle's LineRenderer
    private bool isOn = false;
    private LineRenderer lineRenderer;
    private Transform playerTransform;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Configure the LineRenderer
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = indicatorColor;
        lineRenderer.endColor = indicatorColor;
    }

    private void Update()
    {
        if (isOn)
        {
            // Update the indicator's position to follow the player
            transform.position = playerTransform.position;

            // Redraw the placement circle based on the player's position
            DrawPlacementCircle();
        }
    }

    private void DrawPlacementCircle()
    {
        // Calculate points for a circle based on the player's position
        Vector3[] points = new Vector3[segments + 1];
        float angleIncrement = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleIncrement;
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * placementRadius + playerTransform.position.x;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * placementRadius + playerTransform.position.y;
            points[i] = new Vector3(x, y, 0f);
        }

        // Set the points to the LineRenderer
        lineRenderer.positionCount = segments + 1;
        lineRenderer.SetPositions(points);
    }

    public void enable()
    {
        isOn = true;
    }

    public void disable()
    {
        isOn = false;
        Destroy(lineRenderer);
    }
}
