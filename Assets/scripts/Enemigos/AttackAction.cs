using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : EnemyAction
{
    public override void Act(EnemyStateMachine stateMachine)
    {
        Attack(stateMachine);
    }

    private void Attack(EnemyStateMachine stateMachine)
    {
        stateMachine.enemy.Caminar(true);
        stateMachine.enemy.arma.Disparar((stateMachine.player.transform.position - stateMachine.transform.position));
        Debug.Log("Attacking");
    }
}