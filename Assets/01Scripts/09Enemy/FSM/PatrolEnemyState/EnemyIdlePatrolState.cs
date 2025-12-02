using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdlePatrolState : EnemyStateBase
{
    private EnemyPatrol enemyPatrol;
    public EnemyIdlePatrolState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        enemyPatrol = enemy as EnemyPatrol;
    }
    private float Timer;
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
        Timer = 0;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Timer += Time.deltaTime;
        if (Timer >= enemyPatrol.Data.IdleTime)
        {
            //转换为巡逻状态
            state.ChangeState(enemyPatrol.PatrolPatrolState);
            return;
        }
    }
}
