using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyStateBase
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
    }
    private GameObject Target;
    public override void Enter()
    {
        base.Enter();
        Target = enemy.CheckPlayer();
        enemy.ani.speed = 1.5f;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.ani.speed = 1f;
        enemy.SetVelocityX(0);
    }
    public override void FrameUpdate()
    {
        base.FrameUpdate();
        Target = enemy.CheckPlayer();
        if (Target != null)
        {
            //朝右
            if ((Target.transform.position.x - enemy.Position.x) >= 0.5f)
            {
                enemy.Flip(true);
                enemy.SetVelocityX(enemy.Data.ChaseSpeed);
            }
            //朝左
            else
            {
                enemy.Flip(false);
                enemy.SetVelocityX(-enemy.Data.ChaseSpeed);
            }
            //到达攻击范围后，攻击
            if (CheckIsCanAttack())
            {
                state.ChangeState(enemy.ReactState);
                return;
            }
        }
        else
        {
            state.ChangeState(enemy.IdleState);
            return;
        }
    }
    public virtual bool CheckIsCanAttack()
    {
        return enemy.CheckIsCanAttack();
    }
}
