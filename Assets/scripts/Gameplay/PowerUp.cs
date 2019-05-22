using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class PowerUp : Pooleable
{

    [SerializeField] private PowerUpData powerUp;

    private void OnValidate()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = powerUp.sprite;
        sprite.sortingLayerName = "Elementos";
        GetComponent<BoxCollider2D>().isTrigger = true;
    }


    public override void Activar()
    {
        gameObject.SetActive(true);
    }

    public override void Reiniciar()
    {
    }

    public override void Subscribir(IPooleableCaller caller)
    {
    }

    public override void Desactivar()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            UserHabilidad(player);
            Desactivar();
        }
    }

    private void UserHabilidad(Player player)
    {
        float cantidad = powerUp.cantidad;
        float duracion = powerUp.duracion;
        switch (powerUp.tipo)
        {
            case (TipoPowerUp.ataque):
                player.PowerUpDamage(cantidad, duracion);
                break;
            case(TipoPowerUp.velocidad):
                player.PowerUpSpeed(cantidad,duracion);
                break;
            case (TipoPowerUp.salud):
                player.saludActual += (int)cantidad;
                break;
            case(TipoPowerUp.invulnerabilidad):
                player.SetInvulneravility(true,duracion);
                break;
        }
    }
    
    
}
