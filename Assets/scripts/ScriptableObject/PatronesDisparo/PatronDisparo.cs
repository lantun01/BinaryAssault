using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatronDisparo : ScriptableObject
{
    public abstract Vector3 Posicion(float time, float speed, float angulo);
}
