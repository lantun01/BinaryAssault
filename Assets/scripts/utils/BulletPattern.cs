using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Utils
{
    public static class BulletPattern
    {
        
        public static Vector3 Sinusoidal(float time, float speed, float amplitud, float angulo)
        {
            Vector3 position = new Vector3();
            float y = amplitud*Mathf.Sin(time*speed);
            float x = time * speed;

            float angRad = Mathf.Deg2Rad * angulo;

            position.x = x * Mathf.Cos(angRad) - y * Mathf.Sin(angRad);
            position.y = x * Mathf.Sin(angRad) + y * Mathf.Cos(angRad);
            return position;
        }
    }
}

