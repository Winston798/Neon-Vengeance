using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻击状态基类
public class EnemyAttackState : EnemyStateBase
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocityX(0);
        enemy.IsAttack = true;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.IsAttack = false;
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (AniComplete)
        {
            state.ChangeState(enemy.ChaseState);
        }
    }
}
