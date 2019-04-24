using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    private Vector3 mirada;
    public Vector3 center;
    public override void Act(Player player)
    {
        player.mirada = player.joystick.mirada;
        player.Mover();
        Animar(player);
    }

    public void Disparar(Player player)
    {
        player.Disparar();
    }

    private void Animar(Player player)
    {
        if (player.mirada.x<0)
        {
            player.VoltearSprite(true);
        }
        else
        {
            player.VoltearSprite(false);
        }
        
        if (player.joystick.Pressed)
        {
            player.animator.SetBool(player.hashCaminar,true);
            player.SetEmisionPolvo(true);
        }
        else
        {
            player.animator.SetBool(player.hashCaminar, false);
            player.SetEmisionPolvo(false);
        }
    }
}
