﻿using UnityEngine;
using  Utils;

public class Arma : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Vector3 posicionDerecha;
    public Vector3 posicionIzquierda;
    public Proyectil proyectil;
    [SerializeField]
    private LineRenderer mira;
   [SerializeField] private ArmaData data;
    private int dispararHash;
    private bool armado;
    private Animator animator;
    private bool cooldown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        mira = GetComponent<LineRenderer>();
        dispararHash = Animator.StringToHash("disparar");
        
        if (data)
        {
            SetArma(data);
        }
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
        if (data.tieneMira)
        {
        mira.enabled = true;
        }
    }

    public void DesactivarMira()
    {
        if (data && data.tieneMira)
        {
        mira.enabled = false;
        }
    }

    public void SetArma(ArmaData armaData)
    {
        data = armaData;
        sprite.sprite = data.sprite;
        proyectil = data.proyectil;
    }

    public void ActualizarMira(Vector3 posicion)
    {
        if (data && data.tieneMira)
        {
        mira.SetPosition(0, transform.position);
        mira.SetPosition(1, posicion);
        }
    }

    internal void Disparar(Vector3 mirada)
    {
        if (cooldown || !data) return;
        AnimarDisparar();
        data.Disparar(transform, mirada);
        StartCoroutine(Corroutines.Wait(data.fireRate,() => cooldown = true,()=>cooldown = false));
    }
    
    

    
}
