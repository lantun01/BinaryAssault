using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStateMachine 
{
    public PlayingState playing = new PlayingState();
    public WaitState waiting = new WaitState();
    public Action<Player> act;
    private State currentState;
    public delegate void PostWait();
   

    public void Inicializar()
    {
        act = playing.Disparar;
        currentState = playing;
        
    }

    public void Act(Player player)
    {
        currentState.Act(player);
    }

    public IEnumerator SetWait(float time)
    {
        currentState = waiting;
        act = DoNothing;

        for (int i = 0; i < time*10;i++)
        {
            yield return null;
        }
        currentState = playing;
        act = playing.Disparar;
    }

    public IEnumerator SetWait(float time, PostWait PostWaitAction)
    {
        currentState = waiting;
        act = DoNothing;

        for (int i = 0; i < time * 10; i++)
        {
            yield return null;
        }
        currentState = playing;
        act = playing.Disparar;
        PostWaitAction.Invoke();
    }


    public void DoNothing(Player player)
    {

    }
}
