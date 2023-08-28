using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayActions
{
    // We want to capture player transforms, rotations and eventually keystrokes
    public Vector3 position;
    public Quaternion rotation;

    public bool isMoving;
}
