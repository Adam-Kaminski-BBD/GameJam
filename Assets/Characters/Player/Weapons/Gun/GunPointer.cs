using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointer : MonoBehaviour
{
    public Transform gunTransform; // Reference to the gun's transform

    void Update()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Calculate the vector from the character (parent) to the mouse
        Vector3 characterToMouse = mousePosition - transform.position;

        // Calculate the angle between the character's right direction and the vector to the mouse
        float angle = Mathf.Atan2(characterToMouse.y, characterToMouse.x) * Mathf.Rad2Deg;

        // Flip the gun's X-axis based on whether it's to the left or right of the character
        if (characterToMouse.x < 0)
        {
            gunTransform.localScale = new Vector3(1, -1, 1); // Flip X-axis when left
        }
        else
        {
            gunTransform.localScale = new Vector3(1, 1, 1); // Reset to normal when right
        }

        // Set the rotation of the gun
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
