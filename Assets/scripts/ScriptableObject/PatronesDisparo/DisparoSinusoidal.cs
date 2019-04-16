using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Patron Disparo/Sinusoidal")]
public class DisparoSinusoidal : PatronDisparo
{
    public float amplitud;
    public override Vector3 Posicion(float time, float speed, float angulo)
    {
         Vector3 position = new Vector3();
         position.y = amplitud * Mathf.Sin(time * speed*Mathf.PI);
         position.x = time * speed;
         position.RotarPunto(angulo);
            
         return position;
    }
}
