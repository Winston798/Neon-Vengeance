using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtPatrolState : EnemyStateBase
{
    private EnemyPatrol enemyPatrol;
    public EnemyHurtPatrolState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        enemyPatrol = enemy as EnemyPatrol;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (AniComplete)
        {
            state.ChangeState(enemyPatrol.PatrolIdleState);
        }
    }
}
