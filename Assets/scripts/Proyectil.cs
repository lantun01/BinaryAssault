
using UnityEngine;

public class Proyectil : Pooleable, IUpdateable
{
    private float time;
    public Vector3 posInicial;
    public float angulo;
    private PatronDisparo patron;



    private void Start()
    {
        Subscribir();
    }

    public override void Activar()
    {
        gameObject.SetActive(true);
        time = 0;
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



    public override void Subscribir(IPooleableCaller caller)
    {
    }

    private void OnBecameInvisible()
    {
        Desactivar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Desactivar();
    }

    public void CustomUpdate()
    {
        if (isActiveAndEnabled)
        {
        time += Time.deltaTime;
        transform.position = posInicial + patron.Posicion(time, 8, angulo);
        }
    }

    public void Subscribir()
    {
        UpdateManager.instance.Subscribe(this);
    }
}

