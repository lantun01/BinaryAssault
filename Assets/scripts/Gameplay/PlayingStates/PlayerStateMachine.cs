using System.Collections;
using System;

public class PlayerStateMachine 
{
    public PlayingState playing = new PlayingState();
    public WaitState waiting = new WaitState();
    public Action<Player> act;
    private State currentState;
    public delegate void Resolver();

    private Player player;

    private Action endWaitAction;
    
    public delegate void PlayerAction(Player p);

    public PlayerStateMachine(Player p)
    {
        player = p;
    }
   

    public void Inicializar()
    {
        act = playing.Disparar;
        currentState = playing;
        
    }

    public void Act(Player player)
    {
        currentState.Act(player);
    }

    public IEnumerator SetWaitTime(float time)
    {
        player.animator.SetBool(player.hashCaminar,false);
        currentState = waiting;
        act = DoNothing;

        for (int i = 0; i < time*10;i++)
        {
            yield return null;
        }
        currentState = playing;
        act = playing.Disparar;
    }

    public void SetWait(bool doNothing = true)
    {
        player.animator.SetBool(player.hashCaminar,false);
        currentState = waiting;
        if (doNothing)
            act = DoNothing;
    }

    public void SetAct(Action<Player> a)
    {
        act = a;
    }

    public IEnumerator SetWaitTime(float time, Action PostWaitAction)
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

    public void SetPlaying()
    {
        act = playing.Disparar;
        currentState = playing;
    }

    public void SetTexting()
    {
        
    }
}
