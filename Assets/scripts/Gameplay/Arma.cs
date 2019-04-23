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
    private int dispararHash;
    private bool armado;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mira = GetComponent<LineRenderer>();
        dispararHash = Animator.StringToHash("disparar");
    }

    public void AnimarDisparar()
    {
        animator.SetTrigger(dispararHash);
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
        data?.Disparar(transform, mirada);
    }
}
