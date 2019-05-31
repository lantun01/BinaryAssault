using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : EnemyDecision
{
    public override bool Decide(EnemyStateMachine stateMachine)
    {
        bool targetVisible = Look(stateMachine);
        return targetVisible;
    }

    private bool Look(EnemyStateMachine stateMachine)
    {
        Vector3 playerPosition = stateMachine.enemy.player.transform.position;
        float distance = Vector3.Distance(playerPosition, stateMachine.transform.position);
        if (distance < stateMachine.distanciaAtaque)
        {
            stateMachine.attackTarget = stateMachine.enemy.player.transform;
            return true;
        }
        else return false;
    }
}
