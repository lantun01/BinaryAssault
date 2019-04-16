using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Ex 
{
    public static Vector3 RotarPunto(this ref Vector3 vector, float angle)
    {
        float x = vector.x;
        float y = vector.y;
        float angleRad = Mathf.Deg2Rad * angle;

        vector.x = x * Mathf.Cos(angleRad) - y* Mathf.Sin(angleRad);
        vector.y = x * Mathf.Sin(angleRad) + y * Mathf.Cos(angleRad);

        return vector;
    }
}
