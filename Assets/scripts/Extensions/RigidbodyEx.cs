using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyEx
{
   public static void Mover(this Rigidbody2D rb, Vector3 direcccion, float velocidad)
    {
        rb.velocity = direcccion * velocidad;
    }
}
