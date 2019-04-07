using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : IPooleable
{
    public Rigidbody2D rb;

    public override void Activar()
    {
        gameObject.SetActive(true);
    }


    public override void Desactivar()
    {
        gameObject.SetActive(false);
    }

    public void Proyectar(Vector2 direccion, float velocidad)
    {
        rb.velocity = direccion * velocidad;
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
