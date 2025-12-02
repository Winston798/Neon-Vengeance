using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowSPAttackState : BossSPAttackState
{
    public CowSPAttackState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {

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
            state.ChangeState(Boss.BossIdleState);
        }
    }
    public override void AniEvent()
    {
        //物理检测
        Collider2D coll = Physics2D.OverlapBox(Boss.BossSPAttackPos.position, Boss.BossSPAttackRadius, LayerMask.GetMask("Player"));
        if (coll != null)
        {
            coll.GetComponent<IHurt>()?.Hurt(enemy.transform, enemy.Data.Attack);
        }
    }
}
