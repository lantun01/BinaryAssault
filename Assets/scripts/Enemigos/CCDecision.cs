using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Cuerpo a Cuerpo")]
public class CCDecision : EnemyDecision
{
    public override bool Decide(EnemyStateMachine stateMachine)
    {
        bool targetNear = Look(stateMachine);
        return targetNear;
    }

    private bool Look(EnemyStateMachine stateMachine)
    {
        Vector3 playerPosition = stateMachine.enemy.player.transform.position;
        float distance = Vector3.Distance(playerPosition, stateMachine.transform.position);
        if (distance < stateMachine.distanciaCuerpo)
        {
            stateMachine.attackTarget = stateMachine.enemy.player.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}