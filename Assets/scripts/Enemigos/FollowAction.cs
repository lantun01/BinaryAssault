using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Follow")]
public class FollowAction : EnemyAction
{
    public override void Act(EnemyStateMachine stateMachine)
    {
        Follow(stateMachine);
    }

    private void Follow(EnemyStateMachine stateMachine)
    {
        stateMachine.enemy.Caminar(true);
        Debug.Log("Following");
    }
}
