using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Patron Disparo/Recto")]

public class DisparoRecto : PatronDisparo
{
    public override Vector3 Posicion(float time, float speed, float angulo)
    {
        Vector3 position = new Vector3();
        position.x = time * speed;
        position.RotarPunto(angulo);
        return position;
    }
}
