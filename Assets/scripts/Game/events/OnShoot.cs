using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnShoot : EventArgs
{
    public Vector3 position;
    public Quaternion rotation;

    public OnShoot(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
