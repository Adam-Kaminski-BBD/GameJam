using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_System : MonoBehaviour
{
    public Vector2 gridSize = new Vector2(24, 13.5f);
    public Vector2 cellSize = new Vector2(.16f, .16f);
    public GameObject itemPrefab;
    public GameObject itemPreviewPrefab;
    private GameObject itemPreview;
    private int numColumns;
    private int numRows;
    private int rotationSteps = 0;

    public bool isOn = false;

    public float placementRadius = 5f; // Radius around the player for placement

    private Transform playerTransform; // Reference to the player's transform

    public GameObject player;
    private Player_Replay replay;

    void Start()
    {
        numColumns = (int)gridSize.x;
        numRows = (int)gridSize.y;
        replay = player.GetComponent<Player_Replay>();
        // Find the player's transform using a tag or other method
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
                itemPreview = Instantiate(itemPreviewPrefab);
            }

            Vector3 previewPosition = GridToWorld(new Vector2(gridIndex.x, gridIndex.y));

            // Check the distance between the player and the placement position
            float distanceToPlayer = Vector3.Distance(playerTransform.position, previewPosition);
            if (distanceToPlayer <= placementRadius)
            {
                itemPreview.transform.position = previewPosition;

                float scrollInput = Input.GetAxis("Mouse ScrollWheel");
                if (scrollInput != 0f)
                {
                    rotationSteps += (int)Mathf.Sign(scrollInput);
                    float rotationAngle = rotationSteps * 45f;
                    itemPreview.transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
                }

                if (Input.GetMouseButtonDown(0) && distanceToPlayer <= placementRadius)
                {
                    float finalRotation = rotationSteps * 45f;
                    GameObject placedItem = Instantiate(itemPrefab, previewPosition, Quaternion.Euler(0f, 0f, finalRotation));
                    Destroy(itemPreview);
                    rotationSteps = 0;
                    replay.triggerClickWall(previewPosition, Quaternion.Euler(0f, 0f, finalRotation));
                }
            }
            else
            {
                // Hide the item preview if it's outside the placement radius
                Destroy(itemPreview);
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
