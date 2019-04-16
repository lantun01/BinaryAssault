using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using System;

public class Proyectil : IPooleable
{
    private float time;
    public Vector3 posInicial;
    public float angulo;
    private PatronDisparo patron;

    public override void Activar()
    {
        gameObject.SetActive(true);
        time = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.position = posInicial + patron.Posicion(time, 8 , angulo);
    }


    public override void Desactivar()
    {
        gameObject.SetActive(false);
    }

   

    public void SetPatron(PatronDisparo patron)
    {
        this.patron = patron;
    }

    public override void Reiniciar()
    {
        throw new System.NotImplementedException();
    }



    public override void Subscribir()
    {
        throw new System.NotImplementedException();
    }

    private void OnBecameInvisible()
    {
        Desactivar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Desactivar();
    }
}

