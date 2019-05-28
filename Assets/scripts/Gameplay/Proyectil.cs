
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Proyectil : Pooleable, IUpdateable
{
    private float time;
    public Vector3 posInicial;
    public float angulo;
    private PatronDisparo patron;
    private float damage;


    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void Disparar(float damage, Vector3 direccion)
    {
        this.damage = damage;
        transform.Rotar(direccion);
    }

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



    public override void SubscribirCaller(IPooleableCaller caller)
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

