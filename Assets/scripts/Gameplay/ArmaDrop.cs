using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDrop : MonoBehaviour
{
    public ArmaData data;

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.GetComponent<Player>())
    //    {
    //        collision.gameObject.GetComponent<Player>().CambiarArma(data);
    //        Destroy(gameObject);
    //    }
    //}



    public void RecogerArma(Player player)
    {
        player.AgregarArma(data);
        Destroy(gameObject);
    }
}