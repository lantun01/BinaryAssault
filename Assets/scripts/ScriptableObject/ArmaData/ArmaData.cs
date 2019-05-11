using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmaData : ScriptableObject
{
    public Sprite sprite;
    public Proyectil proyectil;
    public PatronDisparo patron;
    public int damage;
    public float speed;
    [Tooltip("Disparos por segundo")]
    public float fireRate;
    public bool tieneMira;
    public abstract void Disparar(Transform arma, Vector3 direccion);
    
}
