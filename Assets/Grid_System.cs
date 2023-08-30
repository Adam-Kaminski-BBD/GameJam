using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(24, 13.5f); // Number of rows and columns in the grid
    public Vector2 cellSize = new Vector2(.16f, .16f);   // Size of each grid cell

    public GameObject itemPrefab; // Prefab to be placed
    public GameObject itemPreviewPrefab; // Prefab for the preview
    private GameObject itemPreview; // Reference to the preview GameObject
    private int numColumns;
    private int numRows;
    private int rotationSteps = 0; // Current rotation angle

    public bool isOn = false;

    void Start()
    {
        // Initialize missing variables
        numColumns = (int)gridSize.x;
        numRows = (int)gridSize.y;
    }

    void Update()
    {
        if (isOn)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.transform.position.y;

            Vector2 gridIndex = WorldToGrid(Camera.main.ScreenToWorldPoint(mousePosition));

            if (itemPreview == null)
            {
                itemPreview = Instantiate(itemPreviewPrefab); // Instantiate the preview
            }

            Vector3 previewPosition = GridToWorld(new Vector2(gridIndex.x, gridIndex.y));
            itemPreview.transform.position = previewPosition;

            // Rotate the preview sprite by 90 degrees on each scroll step
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                rotationSteps += (int)Mathf.Sign(scrollInput);
                float rotationAngle = rotationSteps * 45f;
                itemPreview.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Place wall brudda");
                // Place the item with the same rotation as the preview
                float finalRotation = rotationSteps * 45f;
                GameObject placedItem = Instantiate(itemPrefab, previewPosition, Quaternion.Euler(0f, 0f, finalRotation));
                Destroy(itemPreview); // Remove the preview when placing the item

                // Reset rotation steps
                rotationSteps = 0;
            }
        }
    }

    public void setOff()
    {
        isOn = false;
    }
    
    public void setOn()
    {
        isOn = true;
    }

    public Vector2 WorldToGrid(Vector3 worldPosition)
    {
        int col = Mathf.FloorToInt((worldPosition.x - transform.position.x) / cellSize.x);
        int row = Mathf.FloorToInt((worldPosition.y - transform.position.y) / cellSize.y);

        col = Mathf.Clamp(col, 0, numColumns - 1);
        row = Mathf.Clamp(row, 0, numRows - 1);

        return new Vector2(col, row);
    }

    public Vector3 GridToWorld(Vector2 gridPosition)
    {
        float x = gridPosition.x * cellSize.x + transform.position.x;
        float y = gridPosition.y * cellSize.y + transform.position.y;

        return new Vector3(x, y, 0);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.grey;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector3 gridPosition = GridToWorld(new Vector2(x, y));
                Gizmos.DrawWireCube(gridPosition, new Vector3(cellSize.x, cellSize.y, 0));
            }
        }
    }
}
