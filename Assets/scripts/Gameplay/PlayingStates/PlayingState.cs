using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : State
{
    private Vector3 mirada;
    public Vector3 center;
    public override void Act(Player player)
    {
        mirada = player.input.mirada;
        player.Mover();

        //Disparar
        if (player.input.disparar)
        {
            Disparar(player);
        }


        if (player.input.dash)
        {
            player.Dash();
        }

        //Skill 1
        if (player.input.skill1)
        {
            RaycastHit2D rc = Physics2D.Raycast(player.transform.position,player.input.mirada, 3f,player.layermask);
            if (rc.collider)
            {
                player.transform.position = rc.point-(player.input.mirada)*0.5f;
            }
            else
            {
                player.transform.position += mirada * 3;
            }
            Debug.Log(rc.point);
            Debug.Log(rc.distance);
            center = rc.point;
        }
        Debug.DrawLine(player.transform.position,(Vector2)player.transform.position + player.input.mirada * 2, Color.red);
        Animar(player);
    }

    public void Disparar(Player player)
    {
        player.Disparar();
    }

    private void Animar(Player player)
    {
        if (player.input.mirada.x<0)
        {
            player.VoltearSprite(true);
        }
        else
        {
            player.VoltearSprite(false);
        }
        
        if (player.input.Moviendo)
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
