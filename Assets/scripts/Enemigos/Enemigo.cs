using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemigo : Pooleable,IUpdateable
{
    public TransformVariable playerTransform;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private IPooleableCaller caller;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame



    public Vector3 TargetPos()
    {
        Vector3 pos = new Vector3();
        pos.x = transform.position.x;
        pos.y = sprite.bounds.min.y;
        return pos;

    }

    public override void Activar()
    {
        gameObject.SetActive(true);
        EnemyManager.instance.AddEnemigo(this);
        Subscribir();
    }

    public override void Reiniciar()
    {

    }

    public override void SubscribirCaller(IPooleableCaller caller)
    {
        this.caller = caller;
    }

    public override void Desactivar()
    {
        gameObject.SetActive(false);
        EnemyManager.instance.RemoveEnemigo(this);
        UpdateManager.instance.Unsubscribe(this);
        caller = null;
        
    }

    public void Morir()
    {
        caller.OnCall();
        Desactivar();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Morir();
        }
    }

    public void CustomUpdate()
    {
        
    }


    public void Subscribir()
    {
       UpdateManager.instance.Subscribe(this);
    }
}
