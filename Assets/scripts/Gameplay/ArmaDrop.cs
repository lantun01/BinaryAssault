using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]
public class ArmaDrop : MonoBehaviour
{
    public ArmaData data;

    private void OnValidate()
    {
        Collider2D coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
    }

    public void RecogerArma(Player player)
    {
        player.AgregarArma(data);
        Destroy(gameObject);
    }
}