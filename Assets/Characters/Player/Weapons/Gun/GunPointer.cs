using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform gunTransform; // Reference to the gun's transform

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 gunToMouse = mousePosition - gunTransform.position;
        float angle = Mathf.Atan2(gunToMouse.y, gunToMouse.x) * Mathf.Rad2Deg;

        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
