using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackNormalState : EnemyAttackState
{
    private EnemyNormal EnemyNormal;
    public EnemyAttackNormalState(Enemy enemy, EnemyStateMachine stateMachine, string AniName) : base(enemy, stateMachine, AniName)
    {
        EnemyNormal = enemy as EnemyNormal;
    }
    public override void AniEvent()
    {
        base.AniEvent();
        //物理检测
        Collider2D coll = Physics2D.OverlapCircle(EnemyNormal.AttackPos.position, enemy.Data.AttackRadius, LayerMask.GetMask("Player"));
        if (coll != null)
        {
            coll.GetComponent<IHurt>()?.Hurt(enemy.transform, enemy.Data.Attack);
        }
    }
}
