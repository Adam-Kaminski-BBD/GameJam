using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public GameObject[] tooltips; // Reference to the tooltip prefabs
    private GameObject currentTooltip; // The currently displayed tooltip

    public void ShowTooltip(int tooltipIndex)
    {
        // Ensure that the requested tooltip index is valid
        if (tooltipIndex >= 0 && tooltipIndex < tooltips.Length)
        {
            if (currentTooltip != null)
                Destroy(currentTooltip);

            // Calculate the position at the top-right corner of the screen
            Vector3 screenPosition = new Vector3(5f, 5f, 0f);

            // Convert screen position to world space (assuming the camera is at position (0, 0, -10))
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0f; // Set the z-coordinate to ensure it's on the same plane as the canvas

            // Instantiate the specified tooltip prefab at the calculated position
            currentTooltip = Instantiate(tooltips[tooltipIndex], worldPosition, Quaternion.identity);
        }
    }




    public void HideTooltip()
    {
        if (currentTooltip != null)
            Destroy(currentTooltip);
    }
}
