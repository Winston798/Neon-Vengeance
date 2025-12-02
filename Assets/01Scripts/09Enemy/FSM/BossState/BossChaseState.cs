using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : EnemyStateBase
{
    private EnemyBoss Boss;

    public BossChaseState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        Boss = enemy as EnemyBoss;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        Boss.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        if (Boss.player == null)
            return;
        //朝右
        if ((Boss.player.transform.position.x - enemy.Position.x) >= 0.5f)
        {
            enemy.Flip(true);
            enemy.SetVelocityX(Boss.Data.ChaseSpeed);
        }
        //朝左
        else
        {
            enemy.Flip(false);
            enemy.SetVelocityX(-Boss.Data.ChaseSpeed);
        }
        if (Boss.CheckIsCanSPAttack())
        {
            state.ChangeState(Boss.BossSPAttackState);
            return;
        }
        if (Boss.CheckIsCanAttack())
        {
            state.ChangeState(Boss.BossAttackState);
            return;
        }
    }

}
