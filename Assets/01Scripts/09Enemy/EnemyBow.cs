using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : Enemy
{
    public Transform BowPos;//弓箭监测点
    public Transform ArrowPos;//箭生成点
    public GameObject ArrowPrefab;//箭预制体
    protected override void InitState()
    {
        IdleState = new EnemyIdleState(this, state, "Idle");
        PatrolState = new EnemyPatrolState(this, state, "Run");
        ChaseState = new EnemyChaseBowState(this, state, "Run");
        ReactState = new EnemyReactState(this, state, "Idle");
        HurtState = new EnemyHurtState(this, state, "Hurt");
        DieState = new EnemyDieState(this, state, "Die");
        AttackState = new EnemyAttackBowState(this, state, "Attack");
        state.Init(IdleState);
    }
    /// <summary>
    /// 重写远程怪的检测玩家方法
    /// </summary>
    /// <returns></returns>
    public override bool CheckIsCanAttack()
    {
        Collider2D collider = null;
        collider = Physics2D.OverlapBox(BowPos.position, Data.BowRadius, LayerMask.GetMask("Player"));
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
        Gizmos.DrawWireCube(BowPos.position, Data.BowRadius);
    }
}
