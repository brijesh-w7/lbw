using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;

    public MyVector3() { }
    public MyVector3(Vector3 v3)
    {

        x = v3.x;
        y = v3.y;
        z = v3.z;
    }
}