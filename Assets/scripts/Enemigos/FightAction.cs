using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Fight")]
public class FightAction : EnemyAction
{
    public override void Act(EnemyStateMachine stateMachine)
    {
        Fight(stateMachine);
    }

    private void Fight(EnemyStateMachine stateMachine)
    {
        stateMachine.enemy.Caminar(false);
        Debug.Log("Fighting");
    }
}