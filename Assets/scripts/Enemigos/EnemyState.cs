using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class EnemyState : ScriptableObject
{
    public EnemyAction[] actions;
    public EnemyTransition[] transitions;

    public void UpdateState(EnemyStateMachine stateMachine)
    {
        DoActions(stateMachine);
        CheckTransitions(stateMachine);
    }

    private void DoActions(EnemyStateMachine stateMachine)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(stateMachine);
        }
    }

    private void CheckTransitions(EnemyStateMachine stateMachine)
    {
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(stateMachine);
            if (decisionSucceeded)
            {
                stateMachine.TransitionToState(transitions[i].trueState);
            }
            else
            {
                stateMachine.TransitionToState(transitions[i].falseState);
            }
        }
    }
}
