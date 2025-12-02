using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
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
        if (Timer >= enemy.Data.IdleTime)
        {
            //转换为巡逻状态
            enemy.state.ChangeState(enemy.PatrolState);
            return;
        }
        if (enemy.CheckPlayer())
        {
            //切换到追赶状态
            state.ChangeState(enemy.ChaseState);
            return;
        }
    }
}
