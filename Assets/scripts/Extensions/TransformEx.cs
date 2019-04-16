using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformEx 
{
    ///<summary>
    ///<para>Rota transform para estar alineado con la dirección</para>
    ///</summary>
    public static void Rotar(this Transform transform,Vector3 direccion)
    {
        float sign = direccion.y < 0 ? -1 : 1;
        float angulo = Vector2.Angle(Vector2.right, direccion)*sign;
        Vector3 rotacionV = new Vector3(0,0,angulo);
        transform.eulerAngles = rotacionV;
    }

    public static float Angulo(this Vector3 direction)
    {
        float sign = direction.y < 0 ? -1 : 1;
        float angulo = Vector3.Angle(Vector2.right, direction) * sign;
        return angulo;
    }
}
