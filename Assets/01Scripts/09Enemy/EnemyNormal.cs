using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormal : Enemy
{
    public Transform AttackPos;//攻击监测点
    protected override void InitState()
    {
        IdleState = new EnemyIdleState(this, state, "Idle");
        PatrolState = new EnemyPatrolState(this, state, "Run");
        ChaseState = new EnemyChaseNormalState(this, state, "Run");
        ReactState = new EnemyReactState(this, state, "Idle");
        HurtState = new EnemyHurtState(this, state, "Hurt");
        DieState = new EnemyDieState(this, state, "Die");
        AttackState = new EnemyAttackNormalState(this, state, "Attack");
        state.Init(IdleState);
    }
    /// <summary>
    /// 重写近程敌人检测是否可以攻击的方法
    /// </summary>
    /// <returns></returns>
    public override bool CheckIsCanAttack()
    {
        Collider2D collider = null;
        collider = Physics2D.OverlapCircle(AttackPos.position, Data.AttackRadius, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(AttackPos.position, Data.AttackRadius);
    }
}
