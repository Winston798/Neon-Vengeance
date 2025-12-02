using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : EnemyStateBase
{
    private EnemyBoss Boss;
    public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        Boss = enemy as EnemyBoss;
    }
    public override void Enter()
    {
        base.Enter();
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
        Collider2D coll = Physics2D.OverlapBox(Boss.BossAttackPos.position, Boss.BossAttackRadius, LayerMask.GetMask("Player"));
        if (coll != null)
        {
            coll.GetComponent<IHurt>()?.Hurt(enemy.transform, enemy.Data.Attack);
        }
    }
}
