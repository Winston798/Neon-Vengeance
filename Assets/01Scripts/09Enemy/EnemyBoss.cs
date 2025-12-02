using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public Transform BossAttackPos;
    public Vector3 BossAttackRadius;
    public Transform BossSPAttackPos;
    public Vector3 BossSPAttackRadius;
    public Transform player { get; set; }//Boss发现玩家后，会一直追逐玩家
    public BossStartState BossStartState { get; protected set; }
    public BossIdleState BossIdleState { get; protected set; }
    public BossChaseState BossChaseState { get; protected set; }
    public BossAttackState BossAttackState { get; protected set; }
    public BossSPAttackState BossSPAttackState { get; protected set; }
    public BossDieState BossDieState { get; protected set; }
    public bool IsCanUseSP { get; protected set; }
    protected float SPCD;
    protected override void Start()
    {
        base.Start();
        UseSPAttack();
    }
    protected override void InitState()
    {

    }
    /// <summary>
    /// 设置玩家目标
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(Transform player)
    {
        this.player = player;
    }
    /// <summary>
    /// 重写检测玩家是否在攻击范围内的方法
    /// </summary>
    /// <returns></returns>
    public override bool CheckIsCanAttack()
    {
        Collider2D collider = null;
        collider = Physics2D.OverlapBox(BossAttackPos.position, BossAttackRadius, 0, LayerMask.GetMask("Player"));
        if (collider != null)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 是否可以使用特技
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckIsCanSPAttack()
    {
        if (!IsCanUseSP)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// 使用特技
    /// </summary>
    public void UseSPAttack()
    {
        IsCanUseSP = false;
        TimerSystem.Instance.AddTask(SPCD, () =>
        {
            IsCanUseSP = true;
        });
    }
    public override void Hurt(Transform pos, float Value)
    {
        if (!IsCanHurt)
            return;
        IsCanHurt = false;
        TimerSystem.Instance.AddTask(0.25f, () => { IsCanHurt = true; });
        CurrentHeathl -= Value;
        if (Data.HitEffect != null)
            Instantiate(Data.HitEffect, Center.position, Quaternion.identity);
        if (CurrentHeathl <= 0)
        {
            state.ChangeState(BossDieState);
            Die();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(BossAttackPos.position, BossAttackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(BossSPAttackPos.position, BossSPAttackRadius);
    }
}
