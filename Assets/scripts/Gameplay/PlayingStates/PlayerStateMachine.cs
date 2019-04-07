using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayingState playing = new PlayingState();
    public WaitState waiting = new WaitState();
    private State currentState;

    public void Inicializar()
    {
        currentState = playing;
    }

    public void Act(Player player)
    {
        currentState.Act(player);
    }

    public IEnumerator SetWait(float time)
    {
        currentState = waiting;

        for (int i = 0; i < time*10;i++)
        {
            yield return null;
        }
        currentState = playing;
    }
}
