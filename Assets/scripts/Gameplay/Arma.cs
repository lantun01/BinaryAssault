using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arma : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 posicionDerecha;
    public Vector3 posicionIzquierda;
    public Proyectil proyectil;
    [SerializeField]
    private LineRenderer mira;
    private ArmaData data;
    private bool armado;

    private void Awake()
    {
        mira = GetComponent<LineRenderer>();
    }

    public void VolvearSprite(bool value)
    {
        sprite.flipY = value;
        if (value)
        {
            transform.localPosition = posicionIzquierda;
        }
        else
        {
            transform.localPosition = posicionDerecha;
        }
    }

    public void ActivarMira()
    {
        mira.enabled = true;
    }

    public void DesactivarMira()
    {
        mira.enabled = false;
    }

    public void SetArma(ArmaData armaData)
    {
        armado = true;
        data = armaData;
        sprite.sprite = data.sprite;
        proyectil = data.proyectil;
    }

    public void ActualizarMira(Vector3 posicion)
    {
        mira.SetPosition(0, transform.position);
        mira.SetPosition(1, posicion);
    }

    internal void Disparar(Vector3 mirada)
    {
        if (armado)
        {
            Proyectil disparo = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
            disparo.transform.position = transform.position;
            disparo.Proyectar(mirada, 10);
        }
    
    }
}
