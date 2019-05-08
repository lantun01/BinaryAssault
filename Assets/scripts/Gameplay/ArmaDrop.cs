using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaDrop : MonoBehaviour
{
    public ArmaData data;

    public void RecogerArma(Player player)
    {
        player.AgregarArma(data);
        Destroy(gameObject);
    }
}