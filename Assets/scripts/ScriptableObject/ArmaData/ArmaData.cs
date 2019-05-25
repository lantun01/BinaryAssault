using System;
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
    public AudioClip audio;
    public abstract void Disparar(Transform arma, Vector3 direccion, float damage);

    [Header("Nombre y descripción del item")]
    public string nombre;
    [TextArea(2,3)]
    [Tooltip("No más de 50 charácteres pls")]
    public string descripcion;

    [Header("Munición en caso de ser utilizada por el player")]
    public int municion;

    public bool municionInfinita;
}
